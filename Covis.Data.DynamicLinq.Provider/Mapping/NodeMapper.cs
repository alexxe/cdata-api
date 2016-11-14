// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberNodeMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The constant node converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Provider.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    ///     The constant node converter.
    /// </summary>
    internal class NodeMapper : INodeVisitor

    {
        private readonly MapperConfiguration mapperConfiguration;

        private Type entryPointType;

        public NodeMapper(MapperConfiguration mapperConfiguration)
        {
            this.mapperConfiguration = mapperConfiguration;
            this.ParameterContext = new Stack<TypeMap>();
            this.NodeContext = new Stack<TypeMap>();
        }

        public NodeMapper(MapperConfiguration mapperConfiguration, Type entryPointType)
            : this(mapperConfiguration)
        {
            var typeMap = this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == entryPointType);
            this.ParameterContext.Push(typeMap);
        }

        public Type TargetType { get; set; }

        
        private Stack<TypeMap> ParameterContext { get; set; }

        private Stack<TypeMap> NodeContext { get; set; }

        public void Visit(BinaryNode node)
        {
        }

        public void EnterContext(CallNode node)
        {
            var typeMap = this.NodeContext.Pop();
            this.ParameterContext.Push(typeMap);
        }

        public void LeaveContext(CallNode node)
        {
            this.ParameterContext.Pop();
        }

        public void EnterContext(ProjectorNode node)
        {
            var typeMap = this.NodeContext.Pop();
            this.ParameterContext.Push(typeMap);
        }

        public void LeaveContext(ProjectorNode node)
        {
            this.ParameterContext.Pop();
        }

        public void Visit(CallNode node)
        {
            var typeMap = this.ParameterContext.Peek();
            this.NodeContext.Push(typeMap);
        }

        public void Visit(SortNode node)
        {
        }

        public void Visit(TakeNode node)
        {
        }

        public void Visit(SkipNode node)
        {
        }

        public void Visit(MemberNode node)
        {
            var typeMap = this.NodeContext.Pop();
            var propertyMap =
                typeMap.GetPropertyMaps()
                    .FirstOrDefault(
                        x => x.DestinationProperty.Name.Equals(node.Member, StringComparison.CurrentCultureIgnoreCase));
            node.Member = propertyMap.SourceMember.Name;

            if (propertyMap.DestinationPropertyType.IsGenericType && typeof(IModelEntity).IsAssignableFrom(propertyMap.DestinationPropertyType.GenericTypeArguments[0]))
            {
                Type entityType;
                if (propertyMap.CustomExpression != null)
                {
                    entityType = propertyMap.CustomExpression.Body.Type.GenericTypeArguments[0];
                }
                else
                {
                    entityType = propertyMap.SourceType.GenericTypeArguments[0];
                }


                var currentTypeMap = this.GetTypeMap(entityType);
                this.NodeContext.Push(currentTypeMap);
            }
            else if (typeof(IModelEntity).IsAssignableFrom(propertyMap.DestinationPropertyType))
            {
                Type entityType;
                if (propertyMap.CustomExpression != null)
                {
                    entityType = propertyMap.CustomExpression.Body.Type;
                }
                else
                {
                    entityType = propertyMap.SourceType;
                }


                var currentTypeMap = this.GetTypeMap(entityType);
                this.NodeContext.Push(currentTypeMap);
            }
            
           
        }

        public void Visit(EntryPointNode node)
        {
            var typeMap =
                this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.DestinationType == node.EntryPointType);
            node.EntryPointType = typeMap.SourceType;
            this.entryPointType = node.EntryPointType;
            this.TargetType = typeMap.DestinationType;
            this.NodeContext.Push(typeMap);
        }

        public void Visit(ConstantNode node)
        {
        }

        public void Visit(ParameterNode node)
        {
            var typeMap = this.ParameterContext.Peek();
            node.Type = typeMap.SourceType;
            this.NodeContext.Push(typeMap);
        }

        public void Visit(ProjectorNode node)
        {
            foreach (var binding in node.Bindings)
            {
                var typeMap =
                    this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == this.entryPointType);
                binding.Value.Accept(this);
            }
        }

        private TypeMap GetTypeMap(Type sourceType)
        {
            return this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
        }

        #region Public Methods and Operators

        #endregion
    }
}