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
using System.Reflection;
using org.bn.attributes;
using org.bn.coders;
using org.bn.metadata;

namespace org.bn.coders.ber
{	
	static class BERCoderUtils
	{
		public static DecodedObject<int> getTagValueForElement(ElementInfo info, int tagClass, int elemenType, int universalTag)
		{
            DecodedObject<int> result = new DecodedObject<int>();
	    // result.Value =  tagClass | elemenType | universalTag;
	    if(universalTag < UniversalTags.LastUniversal) {
        	result.Value = tagClass | elemenType | universalTag;
	    }
	    else
		result = getTagValue ( tagClass , elemenType , universalTag , universalTag, tagClass ) ;
	    
            result.Size = 1;
            if(info.hasPreparedInfo()) 
            {
                ASN1ElementMetadata meta = info.PreparedASN1ElementInfo;
                if ((meta != null) && (meta.HasTag == true)) {
                    if (meta.IsImplicitTag == true)
                    {
                        result = getTagValue(tagClass, elemenType, universalTag, meta.Tag, meta.TagClass);
                    }
                    else
                    {
                        // we do not change the tag here, the provided number will be used when we encapsulate the element                        
                    }
                }
            }
            else 
            {
			    ASN1Element elementInfo = null;
			    if (info.ASN1ElementInfo != null)
			    {
				    elementInfo = info.ASN1ElementInfo;
			    }
			    else 
                if (info.isAttributePresent<ASN1Element>())
			    {
				    elementInfo = info.getAttribute<ASN1Element>();
			    }
    			
			    if (elementInfo != null)
			    {
				    if (elementInfo.HasTag)
				    {
                        result = getTagValue(tagClass,elemenType,universalTag, elementInfo.Tag,elementInfo.TagClass);
                    }
				}
			}
			return result;
		}

        public static DecodedObject<int> getTagValue(int tagClass, int elemenType, int universalTag, int userTag, int userTagClass)
        {
            DecodedObject<int> resultObj = new DecodedObject<int>();
            int result = tagClass | elemenType | universalTag;
            tagClass = userTagClass;
            if (userTag < 31)
            {
                result = tagClass | elemenType | userTag;
                resultObj.Size = 1;
            }
            else
            {
                result = tagClass | elemenType | 0x1F;
                if (userTag < 0x80)
                {
                    result <<= 8;
                    result |= userTag & 0x7F;
                    resultObj.Size = 2;
                }
                else
                    if (userTag < 0x3FFF)
                    {
                        result <<= 16;
                        result |= (((userTag & 0x3FFF) >> 7) | 0x80) << 8;
                        result |= ((userTag & 0x3FFF) & 0x7f);
                        resultObj.Size = 3;
                    }
                    else
                        if (userTag < 0x3FFFF)
                        {
                            result <<= 24;
                            result |= (((userTag & 0x3FFFF) >> 15) | 0x80) << 16;
                            result |= (((userTag & 0x3FFFF) >> 7) | 0x80) << 8;
                            result |= ((userTag & 0x3FFFF) & 0x3f);
                            resultObj.Size = 4;
                        }
            }
            resultObj.Value = result;
            return resultObj;
        }

        public static int encodeLength(int length, System.IO.Stream stream)
        {
            int resultSize = 0;

            if (length < 0)
            {
                throw new System.ArgumentException();
            }
            else if (length < 128)
            {
                stream.WriteByte((byte)length);
                resultSize++;
            }
            else if (length < 256)
            {
                stream.WriteByte((byte)length);
                stream.WriteByte((byte)0x81);
                resultSize += 2;
            }
            else if (length < 65536)
            {
                stream.WriteByte((byte)(length));
                stream.WriteByte((byte)(length >> 8));
                stream.WriteByte((byte)0x82);
                resultSize += 3;
            }
            else if (length < 16777126)
            {
                stream.WriteByte((byte)(length));
                stream.WriteByte((byte)(length >> 8));
                stream.WriteByte((byte)(length >> 16));
                stream.WriteByte((byte)0x83);
                resultSize += 4;
            }
            else
            {
                stream.WriteByte((byte)(length));
                stream.WriteByte((byte)(length >> 8));
                stream.WriteByte((byte)(length >> 16));
                stream.WriteByte((byte)(length >> 24));
                stream.WriteByte((byte)0x84);
                resultSize += 5;
            }
            return resultSize;
        }

        private static int GetBERLength(System.IO.Stream stream, ref int numberOfIndefiniteLengthMarkers)
        {
            long initialPosition = stream.Position;

            try
            {
                int objectLength;
                int lenSize = 0;

                int b = stream.ReadByte();
                lenSize++;

                if ((b & 0x80) != 0)
                {
                    byte count = (byte)(b & 0x7F);
                    lenSize += count;

                    if (count == 0)
                    {                        
                        return lenSize + GetBERIndefiniteLength(stream, ref numberOfIndefiniteLengthMarkers);
                    }

                    objectLength = 0;

                    while (count-- != 0)
                    {
                        objectLength <<= 8;
                        objectLength += (short)(stream.ReadByte() & 0x00FF);
                    }

                }
                else
                {
                    objectLength = (short)(b & 0x00FF);
                }                

                return lenSize + objectLength;
            }
            finally
            {
                stream.Position = initialPosition;
            }
        }

        private static int GetBERIndefiniteLength(System.IO.Stream stream, ref int numberOfIndefiniteLengthMarkers)
        {
            long initialPosition = stream.Position;

            numberOfIndefiniteLengthMarkers++;

            try
            {
                int totalLength = 0;
                int objectTag;

                objectTag = stream.ReadByte();

                while (objectTag != 0)
                {
                    totalLength++;

                    int len = GetBERLength(stream, ref numberOfIndefiniteLengthMarkers);

                    totalLength += len;
                    stream.Position += len;

                    objectTag = stream.ReadByte();                    
                }                

                return totalLength + 2;
            } 
            finally
            {
                stream.Position = initialPosition;
            }
        }

        public static DecodedLength decodeLength(System.IO.Stream stream)
        {
            int numberOfIndefiniteLengthMarkers = 0;
            int result = 0;
            int bt = stream.ReadByte();
            if (bt == -1)
                throw new System.ArgumentException("Unexpected EOF when decoding!");

            int len = 1;
            if (bt < 128)
            {
                result = bt;
            }
            else if (bt == 128)
            {
                result = GetBERIndefiniteLength(stream, ref numberOfIndefiniteLengthMarkers);
            }
            else
            {
                // Decode length bug fixed. Thanks to John 
                for (int i = bt - 128; i > 0; i--)
                {
                    int fBt = stream.ReadByte();
                    if (fBt == -1)
                        throw new System.ArgumentException("Unexpected EOF when decoding!");

                    result = result << 8;
                    result = result | fBt;
                    len++;
                }
            }
            return new DecodedLength(stream, result, len, numberOfIndefiniteLengthMarkers);
        }

	}
}
