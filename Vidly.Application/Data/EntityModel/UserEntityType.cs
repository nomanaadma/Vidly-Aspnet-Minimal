﻿// <auto-generated />
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;
using Vidly.Application.Models;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Vidly.Application.Data.EntityModel
{
    internal partial class UserEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Vidly.Application.Models.User",
                typeof(User),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(int),
                propertyInfo: typeof(User).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(User).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: 0);
            id.TypeMapping = IntTypeMapping.Default.Clone(
                comparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v),
                keyComparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v),
                providerValueComparer: new ValueComparer<int>(
                    (int v1, int v2) => v1 == v2,
                    (int v) => v,
                    (int v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "integer"));
            id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            var email = runtimeEntityType.AddProperty(
                "Email",
                typeof(string),
                propertyInfo: typeof(User).GetProperty("Email", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(User).GetField("<Email>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                maxLength: 255);
            email.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
                comparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                keyComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                providerValueComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "character varying(255)",
                    size: 255));
            email.TypeMapping = ((NpgsqlStringTypeMapping)email.TypeMapping).Clone(npgsqlDbType: NpgsqlTypes.NpgsqlDbType.Varchar);
        email.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

        var isAdmin = runtimeEntityType.AddProperty(
            "IsAdmin",
            typeof(bool),
            propertyInfo: typeof(User).GetProperty("IsAdmin", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(User).GetField("<IsAdmin>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            sentinel: false);
        isAdmin.TypeMapping = NpgsqlBoolTypeMapping.Default.Clone(
            comparer: new ValueComparer<bool>(
                (bool v1, bool v2) => v1 == v2,
                (bool v) => v.GetHashCode(),
                (bool v) => v),
            keyComparer: new ValueComparer<bool>(
                (bool v1, bool v2) => v1 == v2,
                (bool v) => v.GetHashCode(),
                (bool v) => v),
            providerValueComparer: new ValueComparer<bool>(
                (bool v1, bool v2) => v1 == v2,
                (bool v) => v.GetHashCode(),
                (bool v) => v));
        isAdmin.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

        var name = runtimeEntityType.AddProperty(
            "Name",
            typeof(string),
            propertyInfo: typeof(User).GetProperty("Name", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(User).GetField("<Name>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            maxLength: 50);
        name.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
            comparer: new ValueComparer<string>(
                (string v1, string v2) => v1 == v2,
                (string v) => v.GetHashCode(),
                (string v) => v),
            keyComparer: new ValueComparer<string>(
                (string v1, string v2) => v1 == v2,
                (string v) => v.GetHashCode(),
                (string v) => v),
            providerValueComparer: new ValueComparer<string>(
                (string v1, string v2) => v1 == v2,
                (string v) => v.GetHashCode(),
                (string v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "character varying(50)",
                size: 50));
        name.TypeMapping = ((NpgsqlStringTypeMapping)name.TypeMapping).Clone(npgsqlDbType: NpgsqlTypes.NpgsqlDbType.Varchar);
    name.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

    var password = runtimeEntityType.AddProperty(
        "Password",
        typeof(string),
        propertyInfo: typeof(User).GetProperty("Password", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        fieldInfo: typeof(User).GetField("<Password>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
        maxLength: 1024);
    password.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
        comparer: new ValueComparer<string>(
            (string v1, string v2) => v1 == v2,
            (string v) => v.GetHashCode(),
            (string v) => v),
        keyComparer: new ValueComparer<string>(
            (string v1, string v2) => v1 == v2,
            (string v) => v.GetHashCode(),
            (string v) => v),
        providerValueComparer: new ValueComparer<string>(
            (string v1, string v2) => v1 == v2,
            (string v) => v.GetHashCode(),
            (string v) => v),
        mappingInfo: new RelationalTypeMappingInfo(
            storeTypeName: "character varying(1024)",
            size: 1024));
    password.TypeMapping = ((NpgsqlStringTypeMapping)password.TypeMapping).Clone(npgsqlDbType: NpgsqlTypes.NpgsqlDbType.Varchar);
password.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);

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
    runtimeEntityType.AddAnnotation("Relational:TableName", "Users");
    runtimeEntityType.AddAnnotation("Relational:ViewName", null);
    runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

    Customize(runtimeEntityType);
}

static partial void Customize(RuntimeEntityType runtimeEntityType);
}
}
