using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Inv.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.Net.Http.Json;
using System.Text.Json;

namespace Inv.Application.Utility
{
 
    public class Child
    {
        public int claimId { get; set; }
        public bool isChecked { get; set; }
        public string? item { get; set; }
        //public string? mnuMenuText { get; set; } 
        //public decimal MenuPosition { get; set; }
        public int level { get; set; } 
        public int parentId { get; set; }
        //    public int modSerialID { get; set; }
        public List<dynamic>? children { get; set; } = Array.Empty<dynamic>().ToList();
    }
    // Helper class to safely quote identifiers
    public static class SqlHelper
    {
        public static string QuoteIdentifier(string identifier)
        {
            // Prevent SQL injection by enclosing the identifier in square brackets
            return "[" + identifier.Replace("]", "]]") + "]";
        }
    }
}

/*using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Claim
{
    public int ClaimId { get; set; }
    public bool IsChecked { get; set; }
    public string Item { get; set; }
    public int Level { get; set; }
    public int? ParentId { get; set; }
    public List<Claim> Children { get; set; }

    public Claim()
    {
        Children = new List<Claim>();
    }
}

public class Program
{
    public static void Main()
    {
        var claims = new List<Claim>
        {
            new Claim { ClaimId = 1, IsChecked = false, Item = "Inventory", Level = 0 },
            new Claim { ClaimId = 2, IsChecked = false, Item = "Rental", Level = 0 },
            new Claim { ClaimId = 3, IsChecked = false, Item = "Sales", Level = 0 },
            new Claim { ClaimId = 4, IsChecked = false, Item = "Purchasing", Level = 0 },
            new Claim { ClaimId = 5, IsChecked = false, Item = "Operation", Level = 0 },
            new Claim { ClaimId = 6, IsChecked = false, Item = "Asset Management", Level = 0 },
            new Claim { ClaimId = 7, IsChecked = false, Item = "HRM and Payroll", Level = 0 },
            new Claim { ClaimId = 8, IsChecked = false, Item = "Accounts and Finance", Level = 0 },
            new Claim
            {
                ClaimId = 9, IsChecked = false, Item = "Setup", Level = 0,
                Children = new List<Claim>
                {
                    new Claim { ClaimId = 10, IsChecked = false, Item = "User", Level = 1, ParentId = 9 },
                    new Claim { ClaimId = 15, IsChecked = false, Item = "Group", Level = 1, ParentId = 9 }
                }
            }
        };

        InsertClaimsIntoDatabase(claims);
    }

    public static void InsertClaimsIntoDatabase(List<Claim> claims)
    {
        var connectionString = "YourConnectionStringHere";
        var flatList = FlattenHierarchy(claims, null);

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            foreach (var claim in flatList)
            {
                string query = "INSERT INTO Claims (ClaimId, IsChecked, Item, Level, ParentId) VALUES (@ClaimId, @IsChecked, @Item, @Level, @ParentId)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClaimId", claim.ClaimId);
                    command.Parameters.AddWithValue("@IsChecked", claim.IsChecked);
                    command.Parameters.AddWithValue("@Item", claim.Item);
                    command.Parameters.AddWithValue("@Level", claim.Level);
                    command.Parameters.AddWithValue("@ParentId", claim.ParentId.HasValue ? (object)claim.ParentId.Value : DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }
    }

    public static List<Claim> FlattenHierarchy(List<Claim> claims, int? parentId)
    {
        var flatList = new List<Claim>();

        foreach (var claim in claims)
        {
            claim.ParentId = parentId;
            flatList.Add(claim);

            if (claim.Children != null && claim.Children.Count > 0)
            {
                flatList.AddRange(FlattenHierarchy(claim.Children, claim.ClaimId));
            }
        }

        return flatList;
    }
}*/

/*export interface Child
{
    claimId: number;
  isChecked: boolean;
  item: string;
  children: Child[] | null;
}

new Menu { MnuSerialID = 2, ModSerialID = 2, MenuPosition = "2.1", MenuName = "mnuGR", ParentMenuID = 2, Level = 2 },*/
