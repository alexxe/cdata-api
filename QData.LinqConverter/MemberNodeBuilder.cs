// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionParser.cs" company="">
//   
// </copyright>
// <summary>
//   The my expression visitor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq.Expressions;

namespace QData.LinqConverter
{
    /// <summary>
    ///     The my expression visitor.
    /// </summary>
    public class MemberNodeBuilder : ExpressionVisitor
    {
        // : ExpressionVisitor

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionConverter" /> class.
        /// </summary>
        public MemberNodeBuilder()
        {
            this.Path = new Stack<string>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        private Stack<string> Path { get; set; }

        #endregion

        #region Public Methods and Operators

        public string GetPath()
        {
            return string.Join(".", this.Path.ToArray());
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The visit.
        /// </summary>
        /// <param name="exp">
        ///     The exp.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        public override Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return exp;
            }

            switch (exp.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return this.VisitMemberAccess((MemberExpression)exp);
                default:
                    return base.Visit(exp);
            }
        }

        protected virtual Expression VisitMemberAccess(MemberExpression member)
        {
            this.Path.Push(member.Member.Name);
            return base.VisitMember(member);
        }

        #endregion
    }
}