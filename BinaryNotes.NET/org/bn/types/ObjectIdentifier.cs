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
using System.Collections.Generic;
using System.Text;

namespace org.bn.types
{
    public class ObjectIdentifier
    {
        private string oidString;

        public ObjectIdentifier(string oidString)
        {
            Value = oidString;
        }

        public string Value
        {
            get { return oidString; }
            set { oidString = value; }
        }   

        public int[] getIntArray()
        {
            string[] sa = oidString.Split('.');
            int[] ia = new int[sa.Length];
            for (int i=0; i < sa.Length; i++)
            {
                ia[i] = int.Parse(sa[i]);
            }
            return ia;
        }
    }
}
