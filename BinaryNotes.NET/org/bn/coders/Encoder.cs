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
using System.IO;
using org.bn.attributes;
using org.bn.metadata;
using org.bn.types;

namespace org.bn.coders
{
	
	public abstract class Encoder: IASN1TypesEncoder, IEncoder
	{
        public virtual void encode<T>(T obj, System.IO.Stream stream)
		{
            object ob = (object)obj;
			ElementInfo elemInfo = new ElementInfo();
            elemInfo.AnnotatedClass = obj.GetType();
            int sizeOfEncodedBytes = 0;

            if(obj is IASN1PreparedElement) 
            {
                sizeOfEncodedBytes = encodePreparedElement(obj, stream, elemInfo);
            }
            else 
            {
                elemInfo.ASN1ElementInfo = CoderUtils.getAttribute<ASN1Element>(obj.GetType());
			    sizeOfEncodedBytes = encodeClassType(obj, stream, elemInfo);
            }


			if (sizeOfEncodedBytes == 0)
			{
				throw new System.ArgumentException("Unable to find any supported annotation for class type: " + obj.GetType().ToString());
			};
		}
		
		public virtual int encodeClassType(object obj, System.IO.Stream stream, ElementInfo elementInfo)
		{
			int resultSize = 0;
            if(elementInfo.hasPreparedInfo()) {
                resultSize+=elementInfo.PreparedInfo.TypeMetadata.encode(this,obj, stream, elementInfo);
            }        
            else
            if( obj is IASN1PreparedElement) {
                resultSize+=encodePreparedElement(obj, stream, elementInfo);
            }
            else
            if (elementInfo.isAttributePresent<ASN1SequenceOf>())
            {
                resultSize += encodeSequenceOf(obj, stream, elementInfo);
            }
            else
			if (elementInfo.isAttributePresent<ASN1Sequence>())
			{
				resultSize += encodeSequence(obj, stream, elementInfo);
			}
			else
            if (elementInfo.isAttributePresent<ASN1Choice>())
			{
				resultSize += encodeChoice(obj, stream, elementInfo);
			}
			else 
            if (elementInfo.isAttributePresent<ASN1BoxedType>())
			{
				resultSize += encodeBoxedType(obj, stream, elementInfo);
			}
			else 
            if (elementInfo.isAttributePresent<ASN1Enum>())
			{
				resultSize += encodeEnum(obj, stream, elementInfo);
			}
			else 
            if (elementInfo.isAttributePresent<ASN1Boolean>())
			{
				resultSize += encodeBoolean(obj, stream, elementInfo);
			}
			else 
            if (elementInfo.isAttributePresent<ASN1Any>())
			{
				resultSize += encodeAny(obj, stream, elementInfo);
			}
			else 
            if (elementInfo.isAttributePresent<ASN1Integer>())
			{
				resultSize += encodeInteger(obj, stream, elementInfo);
			}
            else
            if (elementInfo.isAttributePresent<ASN1Real>())
            {
                resultSize += encodeReal(obj, stream, elementInfo);
            }
            else 
            if (elementInfo.isAttributePresent<ASN1OctetString>())
			{
				resultSize += encodeOctetString(obj, stream, elementInfo);
			}
			else
            if (elementInfo.isAttributePresent<ASN1BitString>() || obj.GetType().Equals(typeof(BitString)))
            {
                resultSize += encodeBitString(obj, stream, elementInfo);
            }
            else
            if (elementInfo.isAttributePresent<ASN1ObjectIdentifier>() || obj.GetType().Equals(typeof(ObjectIdentifier)))
            {
                resultSize += encodeObjectIdentifier(obj, stream, elementInfo);
            }
            else
            if (elementInfo.isAttributePresent<ASN1String>())
			{
				resultSize += encodeString(obj, stream, elementInfo);
			}
			else 
			if (elementInfo.isAttributePresent<ASN1Null>())
			{
				resultSize += encodeNull(obj, stream, elementInfo);
			}
			else
            if (elementInfo.isAttributePresent<ASN1Element>())
			{
				resultSize += encodeElement(obj, stream, elementInfo);
			}
			else
				resultSize += encodeCSElement(obj, stream, elementInfo);
			return resultSize;
		}
		
		public virtual int encodeCSElement(object obj, System.IO.Stream stream, ElementInfo info)
		{
			if (obj.GetType().Equals(typeof(string)))
			{
				return encodeString(obj, stream, info);
			}
            else
            if (obj.GetType().Equals(typeof(byte)))
            {
                return encodeInteger(obj, stream, info);
            }
            else 
            if (obj.GetType().Equals(typeof(int)))
			{
				return encodeInteger(obj, stream, info);
			}
            else
            if (obj.GetType().Equals(typeof(long)))
            {
                return encodeInteger(obj, stream, info);
            }
            else
            if (obj.GetType().Equals(typeof(BigInteger)))
            {
                return encodeInteger(obj, stream, info);
            }
            else
            if (obj.GetType().Equals(typeof(double)))
            {
                return encodeReal(obj, stream, info);
            }
			else 
            if (obj.GetType().Equals(typeof(bool)))
			{
				return encodeBoolean(obj, stream, info);
			}
			else 
            if (obj.GetType().IsArray)
			{
				return encodeOctetString(obj, stream, info);
			}
            else
            if (obj.GetType().IsEnum)
            {
                return encodeCSEnum(obj, stream, info);
            }
			else
				return 0;
		}

        protected int encodeCSEnum(object obj, Stream stream, ElementInfo info)
        {
            System.Type declaringType = System.Enum.GetUnderlyingType(obj.GetType());
            if (declaringType == typeof(long))
            {
                return encodeInteger((long)obj, stream, info);
            }
            else if (declaringType == typeof(int))
            {
                return encodeInteger((int)obj, stream, info);
            }
            else if (declaringType == typeof(byte))
            {
                return encodeInteger((byte)obj, stream, info);
            }
            else
                return 0;
        }

        public int encodePreparedElement(object obj , Stream stream, ElementInfo elementInfo) 
        {
            IASN1PreparedElement preparedInstance = (IASN1PreparedElement)obj;
            elementInfo.PreparedInstance = (preparedInstance);
            ASN1ElementMetadata elementDataSave = null;
            if(elementInfo.hasPreparedASN1ElementInfo()) {
                elementDataSave = elementInfo.PreparedASN1ElementInfo;
            }        
            elementInfo.PreparedInfo = (preparedInstance.PreparedData);
            if(elementDataSave!=null)
                elementInfo.PreparedASN1ElementInfo = (elementDataSave);
            return preparedInstance.PreparedData.TypeMetadata.encode(
                this, obj, stream, elementInfo
            );        
        }

			
		public virtual object invokeGetterMethodForField(PropertyInfo field, object obj, ElementInfo info)
		{
            MethodInfo method = null;
            if (info!=null && info.hasPreparedInfo())
            {
                method = info.PreparedInfo.IsPresentMethod;
            }
            else 
            {
                method = CoderUtils.findIsPresentMethodForField(field, obj.GetType());
            }
            if (method!=null)
            {
                if ((bool)method.Invoke(obj, null))
                {
                    return field.GetValue(obj, null);
                }
                else
                    return null;
            }
            return field.GetValue(obj, null);
		}
		
		
		public virtual bool invokeSelectedMethodForField(PropertyInfo field, object obj, ElementInfo info)
		{
            if (info!=null && info.hasPreparedInfo())
            {
                return (bool)info.PreparedInfo.invokeIsSelectedMethod (obj, null);
            }
            else
            {
                MethodInfo method = CoderUtils.findIsSelectedMethodForField(field, obj.GetType());
                return (bool)method.Invoke(obj, null);
            }
		}
		
		public virtual int encodeSequence(object obj, System.IO.Stream stream, ElementInfo elementInfo)
		{
			int resultSize = 0;
            PropertyInfo[] fields = elementInfo.getProperties(obj.GetType());
            int fieldIdx = 0;
            foreach (PropertyInfo field in fields)
			{
				resultSize += encodeSequenceField(obj, fieldIdx++, field, stream, elementInfo);
			}
			return resultSize;
		}
		
		public virtual int encodeSequenceField(object obj, int fieldIdx, PropertyInfo field, System.IO.Stream stream, ElementInfo elementInfo)
		{
            ElementInfo info = new ElementInfo();
            info.AnnotatedClass = field;

            if (elementInfo.hasPreparedInfo())
            {
                info.PreparedInfo = elementInfo.PreparedInfo.getPropertyMetadata(fieldIdx);
            }
            else
                info.ASN1ElementInfo = CoderUtils.getAttribute<ASN1Element>(field);

			int resultSize = 0;
            if (CoderUtils.isNullField (field, info))
			{
				return encodeNull(obj, stream, elementInfo);
			}
			else
			{
                object invokeObjResult = invokeGetterMethodForField(field, obj, info);
                if (invokeObjResult != null)
                {
                    if (CoderUtils.isDefaultField(field, info) == true)
                    {
                        bool match;

                        if (this is der.DEREncoder)
                        {
                            // skip the field if the current value equals to the default value (this is optional for BER, but mandatory for DER)
                            object newSequenceInstance = Activator.CreateInstance(obj.GetType());
                            CoderUtils.initDefaultValues(newSequenceInstance);
                            object defaultFieldValue = invokeGetterMethodForField(field, newSequenceInstance, info);

                            IEncoder encoder = new org.bn.coders.ber.BEREncoder();

                            System.IO.MemoryStream outputStream1 = new System.IO.MemoryStream();
                            encoder.encode(invokeObjResult, outputStream1);

                            System.IO.MemoryStream outputStream2 = new System.IO.MemoryStream();
                            encoder.encode(defaultFieldValue, outputStream2);

                            if (outputStream1.Length == outputStream2.Length)
                            {
                                match = true;
                                byte[] ar1 = outputStream1.ToArray();
                                byte[] ar2 = outputStream2.ToArray();
                                for (int i = 0; i < ar1.Length; i++)
                                {
                                    if (ar1[i] != ar2[i])
                                    {
                                        match = false;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // DER mode, but no match, we will encode
                                match = false;
                            }
                        }
                        else
                        {
                            // BER mode, we encode.
                            match = false;
                        }

                        if (match == true)
                        {
                            // the default value is used, therefore we do not encode it
                            resultSize += 0;
                        }
                        else
                        {                            
                            // default value is not used, therefore we encode it                            
                            resultSize += encodeClassType(invokeObjResult, stream, info);
                        }
                        
                    }
                    else
                    {                        
                        resultSize += encodeClassType(invokeObjResult, stream, info);
                    }
                }
                else
                {
                    CoderUtils.checkForOptionalField(field, info);
                }
            }
			return resultSize;
		}

        protected ElementInfo getChoiceSelectedElement(Object obj, ElementInfo elementInfo) {
            ElementInfo info = null;                
            
            PropertyInfo[] fields = elementInfo.getProperties(obj.GetType());

            int fieldIdx = 0;
            foreach ( PropertyInfo field in fields ) 
            {
                info = new ElementInfo();
                info.AnnotatedClass = field;
                if(elementInfo.hasPreparedInfo()) {
                    info.PreparedInfo = (elementInfo.PreparedInfo.getPropertyMetadata(fieldIdx));
                }
                else
                    info.ASN1ElementInfo = CoderUtils.getAttribute<ASN1Element>(field);
            
                if (invokeSelectedMethodForField(field, obj, info)) {
                    break;
                }
                else {
                    info = null;
                }
                fieldIdx++;
            }
            
            if(info==null) {
                throw new System.ArgumentException("The choice '" + obj.ToString() + "' does not have a selected item!");
            }        
            return info;
        }
		
		public virtual int encodeChoice(object obj, System.IO.Stream stream, ElementInfo elementInfo)
		{
            int resultSize = 0;
            ElementInfo info = getChoiceSelectedElement(obj, elementInfo);
            Object invokeObjResult = invokeGetterMethodForField((PropertyInfo)info.AnnotatedClass,obj, info);
            resultSize+=encodeClassType(invokeObjResult, stream, info);
            return resultSize;
		}
		
		
		public virtual int encodeEnum(object obj, System.IO.Stream stream, ElementInfo elementInfo)
		{
			int resultSize = 0;
			PropertyInfo field = 
                obj.GetType().GetProperty("Value");
			object result = invokeGetterMethodForField(field, obj, null);
            Type enumClass = null;
			
			foreach(MemberInfo member in obj.GetType().GetMembers())
			{
				if (member is System.Type)
				{
                    Type cls = (Type) member;
                    if(cls.IsEnum) {
                        enumClass = cls;
					    foreach(FieldInfo enumItem in cls.GetFields())
					    {
                            if (CoderUtils.isAttributePresent<ASN1EnumItem>(enumItem))
						    {
							    if (enumItem.Name.Equals(result.ToString(),StringComparison.CurrentCultureIgnoreCase))
							    {
								    elementInfo.AnnotatedClass = enumItem;
								    break;
							    }
						    }
					    }
					    break;
                    }
				}
			}
			resultSize += encodeEnumItem(result, enumClass, stream, elementInfo);
			return resultSize;
		}
		
		public abstract int encodeEnumItem(object enumConstant, Type enumClass, System.IO.Stream stream, ElementInfo elementInfo);		
		
		public virtual int encodeElement(object obj, System.IO.Stream stream, ElementInfo elementInfo)
		{
			elementInfo.AnnotatedClass = obj.GetType();
			return encodeClassType(obj, stream, elementInfo);
		}
		
		public virtual int encodeBoxedType(object obj, System.IO.Stream stream, ElementInfo elementInfo)
		{
			PropertyInfo field = obj.GetType().GetProperty("Value");
			elementInfo.AnnotatedClass = field;

            if (elementInfo.ASN1ElementInfo == null)
            {
                elementInfo.ASN1ElementInfo = CoderUtils.getAttribute<ASN1Element>(field);
            }
            else
            {
                if (!elementInfo.ASN1ElementInfo.HasTag)
                {
                    ASN1Element fieldInfo = CoderUtils.getAttribute<ASN1Element>(field);
                    if (fieldInfo!=null && fieldInfo.HasTag)
                    {
                        elementInfo.ASN1ElementInfo.HasTag = true;
                        elementInfo.ASN1ElementInfo.TagClass = fieldInfo.TagClass;
                        elementInfo.ASN1ElementInfo.IsImplicitTag = fieldInfo.IsImplicitTag;
                        elementInfo.ASN1ElementInfo.Tag = fieldInfo.Tag;
                    }
                }
            }
			if (CoderUtils.isAttributePresent<ASN1Null>(field))
			{
				return encodeNull(obj, stream, elementInfo);
			}
			else
			{
                return encodeClassType(invokeGetterMethodForField(field, obj, elementInfo), stream, elementInfo);
			}
		}
		
		public abstract int encodeBoolean(object obj, System.IO.Stream stream, ElementInfo elementInfo);
		
		public abstract int encodeAny(object obj, System.IO.Stream stream, ElementInfo elementInfo);
		
		public abstract int encodeNull(object obj, System.IO.Stream stream, ElementInfo elementInfo);
		
		public abstract int encodeInteger(object obj, System.IO.Stream steam, ElementInfo elementInfo);

        public abstract int encodeReal(object obj, System.IO.Stream steam, ElementInfo elementInfo);

		public abstract int encodeOctetString(object obj, System.IO.Stream steam, ElementInfo elementInfo);

        public abstract int encodeBitString(object obj, System.IO.Stream steam, ElementInfo elementInfo);
		
		public abstract int encodeString(object obj, System.IO.Stream steam, ElementInfo elementInfo);
		
		public abstract int encodeSequenceOf(object obj, System.IO.Stream steam, ElementInfo elementInfo);

        public abstract int encodeObjectIdentifier(Object obj, System.IO.Stream stream, ElementInfo elementInfo);
	}
}
