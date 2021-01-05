[![Deploy](https://get.pulumi.com/new/button.svg)](https://app.pulumi.com/new)

# Provision Gcp.IamCustomRole using Pulumi/C#

This project provisions a Google Cloud Platform (GCP) [Google IAM Custom Role](https://cloud.google.com/iam/docs/understanding-custom-roles) using CSharp.

## Deploying the App

To provision your infrastructure, follow the below steps.

### Prerequisites

1. [Install Pulumi](https://www.pulumi.com/docs/get-started/install/)
1. [Install .NET Core 3.0+](https://dotnet.microsoft.com/download)
1. [Install Google Cloud SDK (`gcloud`)](https://cloud.google.com/sdk/docs/downloads-interactive)
1. Configure GCP Auth

    * Login using `gcloud`

        ```bash
        $ gcloud auth login
        $ gcloud config set project <YOUR_GCP_PROJECT_HERE>
        $ gcloud auth application-default login
        ```
    > Note: This auth mechanism is meant for inner loop developer
    > workflows. If you want to run this example in an unattended service
    > account setting, such as in CI/CD, please [follow instructions to
    > configure your service account](https://www.pulumi.com/docs/intro/cloud-providers/gcp/setup/). The
    > service account must have the role `roles` / `iam.roleAdmin`.

### Steps

After cloning this repo, from this working directory, run these commands:

1. Create a new Pulumi stack, which is an isolated deployment target for this example:

    This will initialize the Pulumi program in C#.

    ```cmd
    pulumi stack init
    ```

1. Set the required GCP configuration variables:

    This sets configuration options and default values for the role.

    ```cmd
   pulumi config set gcp:GitCommit: <LAST_GIT_COMMIT_ID_HERE>
   pulumi config set gcp:RoleDescription: <ROLE_DESCRIPTION_HERE>
   pulumi config set gcp:RoleId: <ROLE_ID_HERE>
   pulumi config set gcp:RolePermissions: <ROLE_PERMISSIONS_HERE>
   pulumi config set gcp:RoleTitle: <ROLE_TITLE_HERE>

    :: for a project scoped role
    pulumi config set gcp:project <GCP_PROJECT_ID_HERE>
    pulumi config set gcp:RoleScope: project

    :: for an organization scoped role
    pulumi config set gcp:RoleScope: organization
    pulumi config set gcp:orgid: <GCP_ORGANIZATION_ID_HERE>
    ```

1. Provision the custom role:

    To preview and deploy changes, run `pulumi update` and select "yes."

    The `update` sub-command shows a preview of the resources that will be created
    and prompts on whether to proceed with the deployment. Note that the stack
    itself is counted as a resource, though it does not correspond
    to a physical cloud resource.

    You can also run `pulumi up --diff` to see and inspect the diffs of the
    overall changes expected to take place.

    Running `pulumi up` will deploy the IamCustomRole.

    ```cmd
    pulumi up

Previewing update (dev)

View Live: https://app.pulumi.com/dmanson/iamCustomRole/dev/previews/6d5e5ce8-4a42-4418-a228-58d7adfeab1f

     Type                           Name               Plan
 +   pulumi:pulumi:Stack            iamCustomRole-dev  create
 +   └─ gcp:projects:IAMCustomRole  iamCustomRole      create

Resources:
    + 2 to create

Do you want to perform this update?  [Use arrows to move, enter to select, type to filter]
> yes
  no
  details

View Live: https://app.pulumi.com/dmanson/iamCustomRole/dev/updates/1

     Type                           Name               Status
 +   pulumi:pulumi:Stack            iamCustomRole-dev  created
 +   └─ gcp:projects:IAMCustomRole  iamCustomRole      created

Resources:
    + 2 created

Duration: 7s

1. Once you've finished experimenting, tear down your stack's resources:

    ```cmd
    pulumi destroy --yes
    pulumi stack rm --yes
    ```