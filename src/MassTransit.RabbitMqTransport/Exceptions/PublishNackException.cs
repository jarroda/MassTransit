﻿// Copyright 2007-2015 Chris Patterson, Dru Sellers, Travis Smith, et. al.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.RabbitMqTransport
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown when a message is nack'd by the broker
    /// </summary>
#if !NETCORE    
    [Serializable]
#endif
    public class PublishNackException :
        TransportException
    {
        public PublishNackException()
        {
        }

        public PublishNackException(Uri uri, string message)
            : base(uri, message)
        {
        }
#if !NETCORE
        protected PublishNackException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
#endif
    }
}