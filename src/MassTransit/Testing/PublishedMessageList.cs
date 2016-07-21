// Copyright 2007-2014 Chris Patterson, Dru Sellers, Travis Smith, et. al.
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
namespace MassTransit.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class PublishedMessageList :
        MessageList<IPublishedMessage>,
        IPublishedMessageList
    {
        public PublishedMessageList(TimeSpan timeout)
            : base((int)timeout.TotalMilliseconds)
        {
        }

        public IEnumerable<IPublishedMessage<T>> Select<T>()
            where T : class
        {
            return Select(x => typeof(T).IsAssignableFrom(x.MessageType))
                .Cast<IPublishedMessage<T>>();
        }

        public void Add<T>(PublishContext<T> context)
            where T : class
        {
            Add(new ObservedPublishedMessage<T>(context), context.MessageId);
        }

        public void Add<T>(PublishContext<T> context, Exception exception)
            where T : class
        {
            Add(new ObservedPublishedMessage<T>(context, exception), context.MessageId);
        }


        class MessageIdEqualityComparer :
            IEqualityComparer<IPublishedMessage>
        {
            public bool Equals(IPublishedMessage x, IPublishedMessage y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(IPublishedMessage message)
            {
                return message.Context.GetHashCode();
            }
        }
    }
}