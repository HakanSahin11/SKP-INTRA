using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SKP_IntranetSideAPI.Helper_Classes;
using SKP_IntranetSideAPI.Models;
using static SKP_IntranetSideAPI.Helper_Classes.Salting;
namespace SKP_IntranetSideAPI.Helper_Classes
{
    public class RecoveryKeyGen
    {
        public static RecoveryKeyGenModel GenerateRecovery()
        {
            try {
                List<string> recoveryCodes = new List<string>();
                var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
                Random ran = new Random();
                for (int i = 0; i < 5; i++)
                {
                    string random = "";
                    for (int il = 0; il < 15; il++)
                    {
                        if (ran.Next(0, 2) == 1)
                            random += chars.ElementAt(ran.Next(10));

                        else
                            random += ran.Next(10);
                    }
                    recoveryCodes.Add(random);
                }

                //hashed
                List<string> HashedRecoveryKeys = new List<string>();
                List<string> RecoverySalt = new List<string>();

                foreach (var item in recoveryCodes)
                {
                    var content = HashSalt(item, null);
                    HashedRecoveryKeys.Add(content.Pass);
                    RecoverySalt.Add(content.Salt);
                }
                return new RecoveryKeyGenModel(recoveryCodes, RecoverySalt, HashedRecoveryKeys);
            }
            catch
            {
                throw new Exception("Error Code 5.1 - Error at Helper clases RecoveryKeyGen Error");
            }
        }
    
    }
}
