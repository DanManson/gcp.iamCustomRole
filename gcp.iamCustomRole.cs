using System;
using Pulumi;
using Organizations = Pulumi.Gcp.Organizations;
using Projects = Pulumi.Gcp.Projects;

//  The runtime account must have the roles/iam.roleAdmin permission to provision organization scoped roles.
class gcpIamCustomRole : Stack
{
   Pulumi.Config gcpConfig;
   string roleId;
   string roleTitle;
   string roleDescription;
   string[]? rolePermissions;

   public gcpIamCustomRole()
   {
      gcpConfig = new Config("gcp");
      this.roleId = gcpConfig.Require("RoleId");
      this.roleTitle = gcpConfig.Require("RoleTitle");
      this.rolePermissions = gcpConfig.GetObject<string[]>("RolePermissions");
      this.roleDescription = $"{gcpConfig.Require("RoleDescription")} {System.Environment.NewLine} {DateTime.UtcNow.ToString("s")} - {gcpConfig.Require("GitCommit")}";
      ProvisionRole();
   }

   private void ProvisionRole()
   {
      if (gcpConfig.Require("RoleScope").ToLower() == "project") 
      {
         var projIamCustomRole = new Projects.IAMCustomRole("projIamCustomRole", new Projects.IAMCustomRoleArgs
         {
            Description = this.roleDescription,
            Permissions = this.rolePermissions,
            Project = gcpConfig.Require("project"),
            RoleId = this.roleId,
            Title = this.roleTitle,
         });
      } 
      else
      {
         var orgIamCustomRole = new Organizations.IAMCustomRole("orgIamCustomRole", new Organizations.IAMCustomRoleArgs
         {
            Description = this.roleDescription,
            Permissions = this.rolePermissions,
            OrgId = gcpConfig.Require("orgid"),
            RoleId = this.roleId,
            Title = this.roleTitle,
         });
      }
   }
}
