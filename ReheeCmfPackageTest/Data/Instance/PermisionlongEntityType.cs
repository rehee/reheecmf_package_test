﻿// <auto-generated />
using System;
using System.Reflection;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Permisions;

#pragma warning disable 219, 612, 618
#nullable disable

namespace ReheeCmfPackageTest.Data.Instance
{
    internal partial class PermisionlongEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Permisions.Permision<long>",
                typeof(Permision<long>),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(long),
                propertyInfo: typeof(EntityBase<long>).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EntityBase<long>).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw);
            id.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            var authorizeCreate = runtimeEntityType.AddProperty(
                "AuthorizeCreate",
                typeof(bool),
                propertyInfo: typeof(Permision<long>).GetProperty("AuthorizeCreate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<AuthorizeCreate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            authorizeCreate.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var authorizeDelete = runtimeEntityType.AddProperty(
                "AuthorizeDelete",
                typeof(bool),
                propertyInfo: typeof(Permision<long>).GetProperty("AuthorizeDelete", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<AuthorizeDelete>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            authorizeDelete.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var authorizeRead = runtimeEntityType.AddProperty(
                "AuthorizeRead",
                typeof(bool),
                propertyInfo: typeof(Permision<long>).GetProperty("AuthorizeRead", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<AuthorizeRead>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            authorizeRead.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var authorizeUpdate = runtimeEntityType.AddProperty(
                "AuthorizeUpdate",
                typeof(bool),
                propertyInfo: typeof(Permision<long>).GetProperty("AuthorizeUpdate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<AuthorizeUpdate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            authorizeUpdate.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var entityId = runtimeEntityType.AddProperty(
                "EntityId",
                typeof(long),
                propertyInfo: typeof(Permision<long>).GetProperty("EntityId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<EntityId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            entityId.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var entityName = runtimeEntityType.AddProperty(
                "EntityName",
                typeof(string),
                propertyInfo: typeof(Permision<long>).GetProperty("EntityName", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<EntityName>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            entityName.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var rolesCreate = runtimeEntityType.AddProperty(
                "RolesCreate",
                typeof(string),
                propertyInfo: typeof(Permision<long>).GetProperty("RolesCreate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<RolesCreate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            rolesCreate.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var rolesDelete = runtimeEntityType.AddProperty(
                "RolesDelete",
                typeof(string),
                propertyInfo: typeof(Permision<long>).GetProperty("RolesDelete", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<RolesDelete>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            rolesDelete.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var rolesRead = runtimeEntityType.AddProperty(
                "RolesRead",
                typeof(string),
                propertyInfo: typeof(Permision<long>).GetProperty("RolesRead", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<RolesRead>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            rolesRead.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var rolesUpdate = runtimeEntityType.AddProperty(
                "RolesUpdate",
                typeof(string),
                propertyInfo: typeof(Permision<long>).GetProperty("RolesUpdate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Permision<long>).GetField("<RolesUpdate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            rolesUpdate.AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.None);

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);

            return runtimeEntityType;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", null);
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "PermisionList");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
