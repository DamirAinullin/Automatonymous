// Copyright 2011 Chris Patterson, Dru Sellers
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
namespace Automatonymous
{
    using System;
    using System.Threading.Tasks;


    public interface InstanceLift<T>
        where T : StateMachine
    {
        void Raise(Event @event);

        Task RaiseAsync(Event @event);

        void Raise<TData>(Event<TData> @event, TData value);
        
        Task RaiseAsync<TData>(Event<TData> @event, TData value);

        void Raise(Func<T, Event> eventSelector);

        Task RaiseAsync(Func<T, Event> eventSelector);

        void Raise<TData>(Func<T, Event<TData>> eventSelector, TData data);

        Task RaiseAsync<TData>(Func<T, Event<TData>> eventSelector, TData data);
    }
}