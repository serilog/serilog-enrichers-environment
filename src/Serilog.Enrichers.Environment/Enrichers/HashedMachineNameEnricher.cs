using System;
using System.Security.Cryptography;
using System.Text;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Enrichers
{
    /// <summary>
    /// Enriches log events with a hashed version of a MachineName property containing <see cref="Environment.MachineName"/>.
    /// The hashed version is usefull as a simple installation correlation Id, when you don't want to or can't expose the 
    /// machine name due to privacy issues.
    /// </summary>
    public class HashedMachineNameEnricher : MachineNameEnricher
    {
        readonly HashAlgorithm _hashAlgorithm;

        /// <summary>
        /// Create an enricher that hashes the machine name and adds it as a property. Default hashing algorithm is SHA256
        /// </summary>
        /// <param name="hashAlgorithm"></param>
        public HashedMachineNameEnricher(HashAlgorithm hashAlgorithm = null)
        {
            _hashAlgorithm = hashAlgorithm ?? SHA256.Create();
        }

        public override string PropertyName => "HashedMachineName";
        
        protected override object GeneratePropertyValue()
        {
            var machineName = (string)base.GeneratePropertyValue();
            
            //has it
            return Convert.ToBase64String(
                _hashAlgorithm.ComputeHash(
                    new UTF8Encoding().GetBytes(machineName)));
        }
    }
}