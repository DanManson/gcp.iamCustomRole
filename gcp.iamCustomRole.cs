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
      this.roleDescription = $"{gcpConfig.Require("RoleDescription")}";
      ProvisionRole();
   }

   private void ProvisionRole()
   {
      switch (gcpConfig.Require("RoleScope").ToLower())
      {
         case "project":
            {
               var projIamCustomRole = new Projects.IAMCustomRole("projIamCustomRole",
                  new Projects.IAMCustomRoleArgs
                  {
                     Description = roleDescription,
                     Permissions = rolePermissions,
                     Project = gcpConfig.Require("project"),
                     RoleId = roleId,
                     Title = roleTitle,
                  }
               /// To import an existing role uncomment the following block, update PROJECT_ID AND ROLE_ID then 
               /// run Pulumi Up, details, verify property alignment for import, run Pulumi Up, yes, recomment this block
               // ,new CustomResourceOptions {
               //    ImportId = "projects/[PROJECT_ID]/roles/[ROLE_ID]"
               // }
               );
               break;
            }

         default:
            {
               var orgIamCustomRole = new Organizations.IAMCustomRole("orgIamCustomRole",
               new Organizations.IAMCustomRoleArgs
               {
                  Description = roleDescription,
                  Permissions = rolePermissions,
                  OrgId = gcpConfig.Require("orgid"),
                  RoleId = roleId,
                  Title = roleTitle,
               }
               /// To import an existing role uncomment the following block, update ORG_ID AND ROLE_ID
               // ,new CustomResourceOptions {
               //     ImportId = "organizations/[ORG_ID]/roles/[ROLE_ID]"
               // }
               );
               break;
            }
      }
   }
}