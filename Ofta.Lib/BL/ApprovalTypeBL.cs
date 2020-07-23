using Ofta.Lib.Dal;
using Ofta.Lib.Helper;
using Ofta.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofta.Lib.BL
{
    public interface IApprovalTypeBL
    {
        ApprovalTypeModel Add(ApprovalTypeModel approvalType);
        ApprovalTypeModel Update(ApprovalTypeModel approvalType);
        void Delete(IApprovalTypeKey approvalType);
        ApprovalTypeModel GetData(IApprovalTypeKey approvalType);
        IEnumerable<ApprovalTypeModel> ListData();
    }

    public class ApprovalTypeBL : IApprovalTypeBL
    {
        private IApprovalTypeDal _approvalTypeDal;

        public ApprovalTypeBL(IApprovalTypeDal approvalTypeDal)
        {
            _approvalTypeDal = approvalTypeDal;
        }

        private ApprovalTypeModel Validate(ApprovalTypeModel approvalType)
        {
            approvalType.Empty().Throw("APPROVAL TYPE kosong");
            approvalType.ApprovalTypeID.Empty().Throw("APPROVAL TYPE ID invalid");
            approvalType.ApprovalTypeID.Length.GreaterThan(2).Throw("APPROVAL TYPE ID max length is 2");
            approvalType.ApprovalTypeName.Empty().Throw("APPROVAL TYPE NAME invalid");
            approvalType.ApprovalTypeName.Length.GreaterThan(20).Throw("APPROVAL TYPE NAME max length is 20");

            return approvalType;
        }

        public ApprovalTypeModel Add(ApprovalTypeModel approvalType)
        {
            //      INPUT VALIDATION
            var kt = Validate(approvalType);

            //      BUSINESS VALIDATION
            var approvalTypeDb = _approvalTypeDal.GetData(kt);
            approvalTypeDb.NotEmpty().Throw("APPROVAL TYPE ID already exist");

            //      REPO-OP
            _approvalTypeDal.Insert(kt);

            //      RETURN
            return kt;
        }

        public ApprovalTypeModel Update(ApprovalTypeModel approvalType)
        {
            //      INPUT VALIDATION
            var kt = Validate(approvalType);

            //      BUSINESS VALIDATION
            var approvalTypeDb = _approvalTypeDal.GetData(kt);
            approvalTypeDb.Empty().Throw("APPROVAL TYPE ID not found");

            //      REPO-OP
            _approvalTypeDal.Update(kt);

            //      RETURN
            return kt;
        }

        public void Delete(IApprovalTypeKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("APPROVAL TYPE ID empty");

            //      REPO-OP
            _approvalTypeDal.Delete(key);
        }

        public ApprovalTypeModel GetData(IApprovalTypeKey key)
        {
            //      INPUT VALIDATION
            if (key is null)
                throw new ArgumentException("APPROVAL TYPE ID empty");

            //      REPO-OP
            var result = _approvalTypeDal.GetData(key);

            //      RETURN
            return result;
        }

        public IEnumerable<ApprovalTypeModel> ListData()
        {
            //      REPO-OP
            var result = _approvalTypeDal.ListData();

            //      RETURN
            return result;
        }
    }
}
