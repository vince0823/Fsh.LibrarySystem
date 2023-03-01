using System.Collections.ObjectModel;

namespace FSH.Learn.Shared.Authorization;

public static class FSHAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Import = nameof(Import);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string Tree = nameof(Tree);
    public const string Borrowed = nameof(Borrowed);
    public const string Back = nameof(Back);

    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class FSHResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string RoleMenu = nameof(RoleMenu);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
    public const string Departments = nameof(Departments);
    public const string Menus = nameof(Menus);
    public const string BookRooms = nameof(BookRooms);
    public const string BookShelfs = nameof(BookShelfs);
    public const string BookShelfLayers = nameof(BookShelfLayers);
    public const string Book = nameof(Book);
}

public static class FSHPermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", FSHAction.View, FSHResource.Dashboard),
        new("View Hangfire", FSHAction.View, FSHResource.Hangfire),
        new("View Users", FSHAction.View, FSHResource.Users),
        new("Search Users", FSHAction.Search, FSHResource.Users),
        new("Create Users", FSHAction.Create, FSHResource.Users),
        new("Update Users", FSHAction.Update, FSHResource.Users),
        new("Delete Users", FSHAction.Delete, FSHResource.Users),
        new("Export Users", FSHAction.Export, FSHResource.Users),
        new("View UserRoles", FSHAction.View, FSHResource.UserRoles),
        new("Update UserRoles", FSHAction.Update, FSHResource.UserRoles),
        new("View Roles", FSHAction.View, FSHResource.Roles),
        new("Create Roles", FSHAction.Create, FSHResource.Roles),
        new("Update Roles", FSHAction.Update, FSHResource.Roles),
        new("Delete Roles", FSHAction.Delete, FSHResource.Roles),
        new("View RoleMenu", FSHAction.View, FSHResource.RoleMenu),
        new("Create RoleMenu", FSHAction.Create, FSHResource.RoleMenu),
        new("View RoleClaims", FSHAction.View, FSHResource.RoleClaims),
        new("Update RoleClaims", FSHAction.Update, FSHResource.RoleClaims),
        new("View Products", FSHAction.View, FSHResource.Products, IsBasic: true),
        new("Search Products", FSHAction.Search, FSHResource.Products, IsBasic: true),
        new("Create Products", FSHAction.Create, FSHResource.Products),
        new("Update Products", FSHAction.Update, FSHResource.Products),
        new("Delete Products", FSHAction.Delete, FSHResource.Products),
        new("Export Products", FSHAction.Export, FSHResource.Products),
        new("View Departments", FSHAction.View, FSHResource.Departments, IsBasic: true),
        new("Search Departments", FSHAction.Search, FSHResource.Departments, IsBasic: true),
        new("Create Departments", FSHAction.Create, FSHResource.Departments),
        new("Update Departments", FSHAction.Update, FSHResource.Departments),
        new("Delete Departments", FSHAction.Delete, FSHResource.Departments),
        new("Tree Departments", FSHAction.Tree, FSHResource.Departments, IsBasic: true),
        new("View Menus", FSHAction.View, FSHResource.Menus, IsBasic: true),
        new("Search Menus", FSHAction.Search, FSHResource.Menus, IsBasic: true),
        new("Create Menus", FSHAction.Create, FSHResource.Menus),
        new("Update Menus", FSHAction.Update, FSHResource.Menus),
        new("Delete Menus", FSHAction.Delete, FSHResource.Menus),
        new("Tree Menus", FSHAction.Tree, FSHResource.Menus, IsBasic: true),
        new("View Brands", FSHAction.View, FSHResource.Brands, IsBasic: true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands, IsBasic: true),
        new("Create Brands", FSHAction.Create, FSHResource.Brands),
        new("Update Brands", FSHAction.Update, FSHResource.Brands),
        new("Delete Brands", FSHAction.Delete, FSHResource.Brands),
        new("Generate Brands", FSHAction.Generate, FSHResource.Brands),
        new("Clean Brands", FSHAction.Clean, FSHResource.Brands),
        new("Import Brands", FSHAction.Import, FSHResource.Brands, IsBasic: true),
        new("Export Brands", FSHAction.Export, FSHResource.Brands, IsBasic: true),
        new("View Tenants", FSHAction.View, FSHResource.Tenants, IsRoot: true),
        new("Create Tenants", FSHAction.Create, FSHResource.Tenants, IsRoot: true),
        new("Update Tenants", FSHAction.Update, FSHResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", FSHAction.UpgradeSubscription, FSHResource.Tenants, IsRoot: true),
        new("View BookRooms", FSHAction.View, FSHResource.BookRooms, IsBasic: true),
        new("Search BookRooms", FSHAction.Search, FSHResource.BookRooms, IsBasic: true),
        new("Create BookRooms", FSHAction.Create, FSHResource.BookRooms),
        new("Update BookRooms", FSHAction.Update, FSHResource.BookRooms),
        new("Delete BookRooms", FSHAction.Delete, FSHResource.BookRooms),
        new("View BookShelfs", FSHAction.View, FSHResource.BookShelfs, IsBasic: true),
        new("Search BookShelfs", FSHAction.Search, FSHResource.BookShelfs, IsBasic: true),
        new("Create BookShelfs", FSHAction.Create, FSHResource.BookShelfs),
        new("Update BookShelfs", FSHAction.Update, FSHResource.BookShelfs),
        new("Delete BookShelfs", FSHAction.Delete, FSHResource.BookShelfs),
        new("View BookShelfLayers", FSHAction.View, FSHResource.BookShelfLayers, IsBasic: true),
        new("Search BookShelfLayers", FSHAction.Search, FSHResource.BookShelfLayers, IsBasic: true),
        new("Tree BookShelfLayers", FSHAction.Tree, FSHResource.BookShelfLayers, IsBasic: true),
        new("Create BookShelfLayers", FSHAction.Create, FSHResource.BookShelfLayers),
        new("Update BookShelfLayers", FSHAction.Update, FSHResource.BookShelfLayers),
        new("Delete BookShelfLayers", FSHAction.Delete, FSHResource.BookShelfLayers),

        new("View Book", FSHAction.View, FSHResource.Book, IsBasic: true),
        new("Search Book", FSHAction.Search, FSHResource.Book, IsBasic: true),
        new("Create Book", FSHAction.Create, FSHResource.Book),
        new("Update Book", FSHAction.Update, FSHResource.Book),
        new("Delete Book", FSHAction.Delete, FSHResource.Book),
        new("Borrowed Book", FSHAction.Borrowed, FSHResource.Book),
        new("Back Book", FSHAction.Back, FSHResource.Book),
        new("Export Book", FSHAction.Export, FSHResource.Book, IsBasic: true),
        new("Import Book", FSHAction.Import, FSHResource.Book, IsBasic: true),

    };

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record FSHPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}