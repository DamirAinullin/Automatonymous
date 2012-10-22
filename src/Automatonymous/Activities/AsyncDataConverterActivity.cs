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
namespace Automatonymous.Activities
{
    using System;
    using System.Threading.Tasks;


    public class AsyncDataConverterActivity<TInstance, TData> :
        AsyncActivity<TInstance>
    {
        readonly AsyncActivity<TInstance, TData> _activity;

        public AsyncDataConverterActivity(AsyncActivity<TInstance, TData> activity)
        {
            _activity = activity;
        }

        public void Accept(StateMachineInspector inspector)
        {
            inspector.Inspect(this, x => _activity.Accept(inspector));
        }

        public void Execute(TInstance instance)
        {
            throw new AutomatonymousException("This activity requires a body with the event, but no body was specified.");
        }

        public void Execute<T>(TInstance instance, T value)
        {
            TData dataValue = GetDataValue(value);

            _activity.Execute(instance, dataValue);
        }

        public Task<TInstance> ExecuteAsync(TInstance instance)
        {
            var source = new TaskCompletionSource<TInstance>(TaskCreationOptions.None);
            source.SetException(
                new AutomatonymousException("This activity requires a body with the event, but no body was specified."));
            Task<TInstance> task = source.Task;
            return task;
        }

        public Task<TInstance> ExecuteAsync<TData1>(TInstance instance, TData1 value)
        {
            TData dataValue = GetDataValue(value);

            return _activity.ExecuteAsync(instance, dataValue);
        }

        static TData GetDataValue<T>(T value)
        {
            if (!(value is TData))
            {
                string message = "Expected: " + typeof(TData).FullName + ", Received: " + value.GetType().FullName;
                throw new ArgumentException(message, "value");
            }

            object data = value;

            if (data == null)
                throw new ArgumentNullException("value", "The data argument cannot be null");

            var dataValue = (TData)data;
            return dataValue;
        }
    }
}