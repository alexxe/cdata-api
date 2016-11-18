using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.SqlProvider.builder
{
    using Covis.Data.Json.Contracts;

    public interface IQNodeVisitor
    {
        void VisitBinary(QNode node);

        void VisitMember(QNode node);

        void VisitQuerable(QNode node);

        void VisitMethod(QNode node);

        void EnterContext(QNode node);

        void LeaveContext(QNode node);

        void VisitConstant(QNode node);

        void VisitProjection(QNode node);

        


    }
}
