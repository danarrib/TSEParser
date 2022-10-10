/*
 Copyright 2006-2011 Abdulla Abdurakhmanov (abdulla@latestbit.com)
 Original sources are available at www.latestbit.com

 Licensed under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at

 http://www.apache.org/licenses/LICENSE-2.0

 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
 */
using System;
using System.Numerics;
using System.Reflection;
using System.Collections.Generic;
using org.bn.utils;
using org.bn.attributes;
using org.bn.metadata;
using org.bn.types;

namespace org.bn.coders.ber
{
	
	public class BERDecoder:Decoder
	{
        protected internal virtual DecodedLength decodeLength(System.IO.Stream stream)
        {
            return BERCoderUtils.decodeLength(stream);
        }

        public override DecodedObject<object> decodeTag(System.IO.Stream stream)
        {
            int result = 0;
            int bt = stream.ReadByte();
            if (bt == -1)
                return null;
            result = bt;
            int len = 1;
            int tagValue = bt & 31;
            //bool isPrimitive = (bt & 0x20) == 0;
            if (tagValue == UniversalTags.LastUniversal)
            {
                bt = 0x80;
                while ((bt & 0x80) != 0 && len < 4)
                {
                    result <<= 8;
                    bt = stream.ReadByte();
                    if (bt >= 0)
                    {
                        result |= bt;
                        len++;
                    }
                    else
                    {
                        result >>= 8;
                        break;
                    }
                }
            }

            if (this.ApplicationTagSmallestEncodingSizeRuleRelaxed == true)
            {
                if ((len == 2) && (result >= 0x5F01) && (result <= 0x5F1E))
                {
                    // This is an invalid encoding of application tag, however since it appears in some MRTD objects, we enable rule 'bending'.
                    result = TagClasses.Application + (byte)(result);
                }
            }

            return new DecodedObject<object>(result, len);
        }

        protected bool checkTagForObject(DecodedObject<object> decodedTag, int tagClass, int elementType, int universalTag, ElementInfo elementInfo)
        {
            if (decodedTag == null)
                return false;
            int definedTag = BERCoderUtils.getTagValueForElement(elementInfo, tagClass, elementType, universalTag).Value;
            return definedTag == (int)decodedTag.Value;
        }

		public override DecodedObject<object> decodeSequence(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
            bool isSet = false;
            if (!CoderUtils.isSequenceSet(elementInfo))
            {
                if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Constructed, UniversalTags.Sequence, elementInfo))
                {
                    return null;
                }
            }
            else
            {
                if (checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Constructed, UniversalTags.Set, elementInfo))
                {
                    isSet = true;
                }
                else
                    return null;
            }
            using (DecodedLength len = decodeLength(stream))
            {
                int saveMaxAvailableLen = elementInfo.MaxAvailableLen;
                elementInfo.MaxAvailableLen = (len.Value);

                DecodedObject<object> result = null;
                if (!isSet)
                    result = base.decodeSequence(decodedTag, objectClass, elementInfo, stream);
                else
                    result = decodeSet(decodedTag, objectClass, elementInfo, len.Value, stream);
                if (result.Size != len.Value)
                    throw new System.ArgumentException("Sequence '" + objectClass.ToString() + "' size is incorrect!");
                result.Size = result.Size + len.Size;
                elementInfo.MaxAvailableLen = (saveMaxAvailableLen);
                return result;
            }
            
		}

        protected virtual DecodedObject<object> decodeSet(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, int len, System.IO.Stream stream)
        {
            object sequence = createInstanceForElement(objectClass,elementInfo);
            initDefaultValues(sequence);
            DecodedObject<object> fieldTag = null;
            int sizeOfSequence = 0;
            int maxSeqLen = elementInfo.MaxAvailableLen;

            if (maxSeqLen == -1 || maxSeqLen > 0)
            {
                fieldTag = decodeTag(stream);
                if (fieldTag != null)
                    sizeOfSequence += fieldTag.Size;
            }
            PropertyInfo[] fields =
                elementInfo.getProperties(objectClass);

            bool fieldEncoded = false;
            do
            {
                for (int i = 0; i < fields.Length; i++)
                {
                    PropertyInfo field = fields[i];
                    DecodedObject<object> obj = decodeSequenceField(
                        fieldTag, sequence, i, field, stream, elementInfo, false
                    );
                    if (obj != null)
                    {
                        fieldEncoded = true;
                        sizeOfSequence += obj.Size;

                        bool isAny = false;
                        if (i + 1 == fields.Length - 1)
                        {
                            ElementInfo info = new ElementInfo();
                            info.AnnotatedClass = (fields[i + 1]);
                            info.MaxAvailableLen = (elementInfo.MaxAvailableLen);
                            if (elementInfo.hasPreparedInfo())
                            {
                                info.PreparedInfo = (elementInfo.PreparedInfo.getPropertyMetadata(i + 1));
                            }
                            else
                                info.ASN1ElementInfo = CoderUtils.getAttribute<ASN1Element>(fields[i + 1]);
                            isAny = CoderUtils.isAnyField(fields[i + 1], info);
                        }

                        if (maxSeqLen != -1)
                        {
                            elementInfo.MaxAvailableLen = (maxSeqLen - sizeOfSequence);
                        }

                        if (!isAny)
                        {
                            if (i < fields.Length - 1)
                            {
                                if (maxSeqLen == -1 || elementInfo.MaxAvailableLen > 0)
                                {
                                    fieldTag = decodeTag(stream);
                                    if (fieldTag != null)
                                        sizeOfSequence += fieldTag.Size;
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                    fieldTag = null;
                            }
                            else
                                break;
                        }
                    }
                    ;
                }
            }
            while (sizeOfSequence < len && fieldEncoded);
            return new DecodedObject<object>(sequence, sizeOfSequence);
        }

        public override DecodedObject<object> decodeChoice(DecodedObject<object> decodedTag, Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
        {
            if ( (elementInfo.hasPreparedInfo() && elementInfo.hasPreparedASN1ElementInfo() && elementInfo.PreparedASN1ElementInfo.HasTag)
                || (elementInfo.ASN1ElementInfo != null && elementInfo.ASN1ElementInfo.HasTag)) 
            {
                if (!checkTagForObject(decodedTag, TagClasses.ContextSpecific, ElementType.Constructed, UniversalTags.LastUniversal, elementInfo))
                    return null;

                using (DecodedLength len = decodeLength(stream))
                {
                    DecodedObject<object> childDecodedTag = decodeTag(stream);
                    DecodedObject<object> result = base.decodeChoice(childDecodedTag, objectClass, elementInfo, stream);
                    result.Size += len.Size + childDecodedTag.Size;
                    return result;
                }
            }
            else
                return base.decodeChoice(decodedTag, objectClass, elementInfo, stream);
        }

		
		
		public override DecodedObject<object> decodeEnumItem(DecodedObject<object> decodedTag, System.Type objectClass, System.Type enumClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
			if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.Enumerated, elementInfo))
				return null;
			return decodeIntegerValue(stream);
		}
		
		public override DecodedObject<object> decodeBoolean(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
			if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.Boolean, elementInfo))
				return null;
			DecodedObject<object> result = decodeIntegerValue(stream);
			int val = (int) result.Value;
			if (val != 0)
				result.Value  = true;
			else
				result.Value = false;
			return result;
		}
		
		public override DecodedObject<object> decodeAny(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
            int bufSize = elementInfo.MaxAvailableLen;
            if (bufSize == 0)
                return null; 
            System.IO.MemoryStream anyStream = new System.IO.MemoryStream(1024);
            /*int tagValue = (int)decodedTag.Value;
            for (int i = 0; i < decodedTag.Size; i++)
            {
                anyStream.WriteByte((byte)tagValue);
                tagValue = tagValue >> 8;
            }*/

            if(bufSize<0)
                bufSize = 1024;
            int len = 0;
            if (bufSize > 0)
            {
                byte[] buffer = new byte[bufSize];
                int readed = stream.Read(buffer, 0, buffer.Length);
                while (readed > 0)
                {
                    anyStream.Write(buffer, 0, readed);
                    len += readed;
                    if (elementInfo.MaxAvailableLen > 0)
                        break;
                    readed = stream.Read(buffer, 0, buffer.Length);
                }
            }
            CoderUtils.checkConstraints(len, elementInfo);
			return new DecodedObject<object>(anyStream.ToArray(), len);
		}
		
		public override DecodedObject<object> decodeNull(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
			if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.Null, elementInfo))
				return null;
			stream.ReadByte(); // ignore null length
            object obj = createInstanceForElement(objectClass, elementInfo);
			DecodedObject<object> result = new DecodedObject<object>(obj, 1);
			return result;
		}
		
		public override DecodedObject<object> decodeInteger(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
			if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.Integer, elementInfo))
				return null;
            if (objectClass.Equals(typeof(int)))
            {
                DecodedObject<object> result = decodeIntegerValue(stream);
                CoderUtils.checkConstraints((int)result.Value, elementInfo);
                return result;
            }
            else if(objectClass.Equals(typeof(long)))
            {
                DecodedObject<object> result = decodeLongValue(stream);
                CoderUtils.checkConstraints((long)result.Value, elementInfo);
                return result;
            }
            else
            {
                DecodedObject<object> result = decodeBigIntegerValue(stream);
                //CoderUtils.checkConstraints(result.Value, elementInfo);
                return result;
            }
		}

        public override DecodedObject<object> decodeReal(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
        {
            if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.Real, elementInfo))
                return null;

            using (DecodedLength len = decodeLength(stream))
            {
                int realPreamble = stream.ReadByte();

                Double result = 0.0D;
                int szResult = len.Value;
                if ((realPreamble & 0x40) == 1)
                {
                    // 01000000 Value is PLUS-INFINITY
                    result = Double.PositiveInfinity;
                }
                if ((realPreamble & 0x41) == 1)
                {
                    // 01000001 Value is MINUS-INFINITY
                    result = Double.NegativeInfinity;
                    szResult += 1;
                }
                else
                    if (len.Value > 0)
                {
                    int szOfExp = 1 + (realPreamble & 0x3);
                    int sign = realPreamble & 0x40;
                    int ff = (realPreamble & 0x0C) >> 2;
                    DecodedObject<object> exponentEncFrm = decodeLongValue(stream, new DecodedLength(stream, szOfExp));
                    long exponent = (long)exponentEncFrm.Value;
                    DecodedObject<object> mantissaEncFrm = decodeLongValue(stream, new DecodedLength(stream, szResult - szOfExp - 1));
                    // Unpack mantissa & decrement exponent for base 2
                    long mantissa = (long)mantissaEncFrm.Value << ff;
                    while ((mantissa & 0x000ff00000000000L) == 0x0)
                    {
                        exponent -= 8;
                        mantissa <<= 8;
                    }
                    while ((mantissa & 0x0010000000000000L) == 0x0)
                    {
                        exponent -= 1;
                        mantissa <<= 1;
                    }
                    mantissa &= 0x0FFFFFFFFFFFFFL;
                    long lValue = (exponent + 1023 + 52) << 52;
                    lValue |= mantissa;
                    if (sign == 1)
                    {
                        lValue = (long)((ulong)lValue | 0x8000000000000000L);
                    }
#if PocketPC
                    byte[] dblValAsBytes = System.BitConverter.GetBytes(lValue);
                    result = System.BitConverter.ToDouble(dblValAsBytes, 0);
#else            
                    result = System.BitConverter.Int64BitsToDouble(lValue);
#endif
                }
                return new DecodedObject<object>(result, len.Value + len.Size);
            }
        }

        protected DecodedObject<object> decodeLongValue(System.IO.Stream stream)
        {
            using (DecodedLength len = decodeLength(stream))
            {
                return decodeLongValue(stream, len);
            }
        }

        protected DecodedObject<object> decodeBigIntegerValue(System.IO.Stream stream)
        {
            using (DecodedLength len = decodeLength(stream))
            {
                return decodeBigIntegerValue(stream, len);
            }
        }

        protected DecodedObject<object> decodeIntegerValue(System.IO.Stream stream)
        {
            DecodedObject<object> result = new DecodedObject<object>();
            using (DecodedLength len = decodeLength(stream))
            {
                int val = 0;
                for (int i = 0; i < len.Value; i++)
                {
                    int bt = stream.ReadByte();
                    if (bt == -1)
                    {
                        throw new System.ArgumentException("Unexpected EOF when encoding!");
                    }
                    if (i == 0 && (bt & (byte)0x80) != 0)
                    {
                        bt = bt - 256;
                    }

                    val = (val << 8) | bt;
                }
                result.Value = val;
                result.Size = len.Value + len.Size;
                return result;
            }
        }

		protected internal virtual DecodedObject<object> decodeLongValue(System.IO.Stream stream, DecodedLength len)
        {
            DecodedObject<object> result = new DecodedObject<object>();
            long val = 0;
            for (int i = 0; i < len.Value; i++)
            {
                long bt = stream.ReadByte();
                if (bt == -1)
                {
                    throw new System.ArgumentException("Unexpected EOF when encoding!");
                }
                if (i == 0 && (bt & (byte)0x80) != 0)
                {
                    bt = bt - 256;
                }

                val = (val << 8) | bt;
            }
            result.Value = val;
            result.Size = len.Value + len.Size;
            return result;
        }

        protected internal virtual DecodedObject<object> decodeBigIntegerValue(System.IO.Stream stream, DecodedLength len)
        {
            DecodedObject<object> result = new DecodedObject<object>();
            List<byte> bigIntegerByteData = new List<byte>();            
            for (int i = 0; i < len.Value; i++)
            {
                long bt = stream.ReadByte();
                if (bt == -1)
                {
                    throw new System.ArgumentException("Unexpected EOF when encoding!");
                }
                bigIntegerByteData.Insert(0, (byte)bt);
            }            
            result.Value = new BigInteger(bigIntegerByteData.ToArray());            
            result.Size = len.Value + len.Size;
            return result;
        }

		public override DecodedObject<object> decodeOctetString(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
			if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.OctetString, elementInfo))
				return null;
            using (DecodedLength len = decodeLength(stream))
            {
                CoderUtils.checkConstraints(len.Value, elementInfo);
                byte[] byteBuf = new byte[len.Value];
                stream.Read(byteBuf, 0, byteBuf.Length);
                return new DecodedObject<object>(byteBuf, len.Value + len.Size);
            }
		}

		public override DecodedObject<object> decodeBitString(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
			if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.Bitstring, elementInfo))
				return null;

            using (DecodedLength len = decodeLength(stream))
            {
                int trailBitCnt = stream.ReadByte();
                CoderUtils.checkConstraints(len.Value * 8 - trailBitCnt, elementInfo);
                byte[] byteBuf = new byte[len.Value - 1];
                stream.Read(byteBuf, 0, byteBuf.Length);
                return new DecodedObject<object>(new BitString(byteBuf, trailBitCnt), len.Value + len.Size);
            }
		}
		
		public override DecodedObject<object> decodeString(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
			if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, CoderUtils.getStringTagForElement(elementInfo), elementInfo))
				return null;
            using (DecodedLength len = decodeLength(stream))
            {
                CoderUtils.checkConstraints(len.Value, elementInfo);
                byte[] byteBuf = new byte[len.Value];
                stream.Read(byteBuf, 0, byteBuf.Length);
                string result = CoderUtils.bufferToASN1String(byteBuf, elementInfo);
                return new DecodedObject<object>(result, len.Value + len.Size);
            }
		}
		
		public override DecodedObject<object> decodeSequenceOf(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
		{
            if (!CoderUtils.isSequenceSetOf(elementInfo))
            {
                if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Constructed, UniversalTags.Sequence, elementInfo))
                    return null;
            }
            else
            {
                if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Constructed, UniversalTags.Set, elementInfo))
                    return null;
            }

            Type paramType = (System.Type)objectClass.GetGenericArguments()[0];
            Type collectionType = typeof(List<>);
            Type genCollectionType = collectionType.MakeGenericType(paramType);
            Object param = Activator.CreateInstance(genCollectionType);

            using (DecodedLength len = decodeLength(stream))
            {
                if (len.Value != 0)
                {
                    int lenOfItems = 0;
                    int itemsCnt = 0;
                    do
                    {
                        ElementInfo info = new ElementInfo();
                        info.ParentAnnotatedClass = elementInfo.AnnotatedClass;
                        info.AnnotatedClass = paramType;

                        if (elementInfo.hasPreparedInfo())
                        {
                            ASN1SequenceOfMetadata seqOfMeta = (ASN1SequenceOfMetadata)elementInfo.PreparedInfo.TypeMetadata;
                            info.PreparedInfo = (seqOfMeta.getItemClassMetadata());
                        }


                        MethodInfo method = param.GetType().GetMethod("Add");

                        PropertyInfo[] fields = info.getProperties(objectClass);
                        bool isAny = CoderUtils.isAttributePresent<ASN1Any>(elementInfo.AnnotatedClass);
                        bool isSetOf = ((ASN1SequenceOfMetadata)elementInfo.PreparedInfo.TypeMetadata).IsSetOf;

                        // check if we're dealing with 'SET OF ANY' case
                        if ((isSetOf == true) && (isAny == true))
                        {
                            elementInfo.MaxAvailableLen -= (decodedTag.Size + len.Size);
                            DecodedObject<object> obj = decodeAny(decodedTag, objectClass, elementInfo, stream);
                            method.Invoke(param, new object[] { obj.Value });
                            lenOfItems += (obj.Size);
                            itemsCnt++;
                        }
                        else
                        {
                            DecodedObject<object> itemTag = decodeTag(stream);
                            DecodedObject<object> item = decodeClassType(itemTag, paramType, info, stream);
                            if (item != null)
                            {
                                lenOfItems += (item.Size + itemTag.Size);
                                method.Invoke(param, new object[] { item.Value });
                                itemsCnt++;
                            }
                        }

                    }
                    while (lenOfItems < len.Value);
                    CoderUtils.checkConstraints(itemsCnt, elementInfo);
                }
                return new DecodedObject<object>(param, len.Value + len.Size);
            }
		}

        public override DecodedObject<object> decodeObjectIdentifier(DecodedObject<object> decodedTag, System.Type objectClass, ElementInfo elementInfo, System.IO.Stream stream)
        {
            if (!checkTagForObject(decodedTag, TagClasses.Universal, ElementType.Primitive, UniversalTags.ObjectIdentifier, elementInfo))
                return null;
            using (DecodedLength len = decodeLength(stream))
            {
                byte[] byteBuf = new byte[len.Value];
                stream.Read(byteBuf, 0, byteBuf.Length);
                string dottedDecimal = BERObjectIdentifier.Decode(byteBuf);
                return new DecodedObject<object>(new ObjectIdentifier(dottedDecimal), len.Value + len.Size);
            }
        }
	}
}
