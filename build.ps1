$commit = git rev-parse HEAD
Pulumi stack select proj.dev
Pulumi config set gcp:GitCommit $commit
