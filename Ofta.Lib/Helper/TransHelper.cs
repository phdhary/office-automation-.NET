using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ofta.Lib.Helper
{
    public static class TransHelper
    {
        public static TransactionScope NewScope(IsolationLevel isolationLevel)
        {
            return new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = isolationLevel
                });
        }

        public static TransactionScope NewScope()
        {
            return new TransactionScope(TransactionScopeOption.Required,
                new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted
                });
        }
    }
}
