// Copyright 2013-2022 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with an EnvironmentUserName property containing [<see cref="Environment.UserDomainName"/>\]<see cref="Environment.UserName"/>.
    /// </summary>
    public class EnvironmentUserNameEnricher : CachedPropertyEnricher
    {
        /// <summary>
        /// The property name added to enriched log events.
        /// </summary>
        public const string EnvironmentUserNamePropertyName = "EnvironmentUserName";

        protected override LogEventProperty CreateProperty(ILogEventPropertyFactory propertyFactory)
        {
#if ENV_USER_NAME
            var userDomainName = Environment.UserDomainName;
            var userName = Environment.UserName;
#else
            var userDomainName = Environment.GetEnvironmentVariable("USERDOMAIN");
            var userName = Environment.GetEnvironmentVariable("USERNAME");
#endif
            var environmentUserName =  !string.IsNullOrWhiteSpace(userDomainName) ? $@"{userDomainName}\{userName}" : userName;

            return propertyFactory.CreateProperty(EnvironmentUserNamePropertyName, environmentUserName);
        }
    }
}
