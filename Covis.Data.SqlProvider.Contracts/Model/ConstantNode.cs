// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantNode.cs" company="">
//   
// </copyright>
// <summary>
//   The constant node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider.Contracts.Model
{
    /// <summary>
    ///     The constant node.
    /// </summary>
   
    public class ConstantNode : INode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConstantNode" /> class.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        public ConstantNode(object value)
        {
            //if (value is JArray)
            //{
            //    var list = new List<string>();
            //    list.AddRange(((JArray)value).Values<string>());
            //    value = list;
            //}

            this.Value = value;
        }

        #endregion

        #region Public Properties

       
        public bool IsEmpty { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        
        public object Value { get; set; }

        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}