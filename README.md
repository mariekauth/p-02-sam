# AWS Serverless Application Model (SAM)

## Example Objective
- Create an AWS Lambda function (Runtime: dotnet6)
- Create an API Gateway
- Use IaC (Infrastructure as Code) to create and deploy AWS resources in a reproducible fashion.
- Not get charged for any resources.
- Plan to extend this and use it in a project, so confirmed ability to send parameters. (Could also be sent in path).
## Other options, and why SAM
[AWS CodeStar](https://aws.amazon.com/codestar/) would set things up faster. But it would set everything up and provision resources immediately.

[AWS Sam](https://aws.amazon.com/serverless/sam/) allows you to get started quickly with templates (yes, you can build your own). However, it has the added benefit of allowing you to test locally before provisioning the resources in the cloud.

I chose to use AWS SAM:
- Quick setup for multiple scenarios
- Can test locally before provisioning resources (cost savings)
- Will integrate with Terraform which is a cloud agnostic tool for generating infrastructure as code (IaC)
- Ultimately it compiles to Cloud Formation. It will create a Cloud Formation Stack
- Can make changes before deploying. (Change the desired stack, choose deployment strategy, select pipeline, etc)

[AWS Cloud9](https://aws.amazon.com/cloud9/) provides you with an IDE hosted on an EC2 instance with S3 storage. You can even setup a different Cloud9 instance for each project. However, you will get charged for compute time and storage.
## Prerequisites
- IDE (VSCode)
- AWS CLI
- AWS SAM CLI
- Docker
- Dotnet CLI
## Steps Taken
1. Create Directory
2. Open with VSCode
3. Open a Terminal (bash) in VSCode
4. git init
5. echo "# {Project Name}" > README.md
6. dotnet new gitignore
7. Add .vscode/ to .gitignore
8. Add LICENSE
9. git add .
10. git commit -m "Initial Commit"
11. sam init
12. 1 - AWS Quick Start Templates
13. Use the most popular runtime and package type? (Python and zip) [y/N]: N
14. Select Option 2 - dotnet6
15. Select Option 1 - Zip
16. Would you like to enable X-Ray tracing on the function(s) in your application?  [y/N]: N
17. Would you like to enable monitoring using CloudWatch Application Insights? N
18. Project name [sam-app]: sam-app-dotnet6
19. git add .
20. git commit -m "Add SAM Hello World dotnet6"
21. cd sam-app-dotnet6 && sam validate
22. sam build
23. sam local start-lambda
24. sam local invoke HelloWorldFunction
25. Press [Ctrl][C] to stop it
26. sam local start-api
27. git add .
28. git commit -m "After sam build and starting the api"
29. git commit -m "Modified to accept parameters"
30. git commit -m "Modified README.md"

## Notes/Warnings
- The SAM Template does not list all the resources that will be provisioned.
- Please verify your location(s), example: us-east-2
- When in the Console, and looking at resources, if you look in a different region, the resource may not show up even if it is provisioned.
- I have created EC2 instances, functions, policies, gateways, databases, auto-scaling, load balancers, etc. in the Console - cleaning up the resources as I go. I have created CloudFormation stacks, deployed them, stopped them, and removed them.
- I have not added the deployment pipeline or deployed this version (using SAM) because it is a sample of how to test locally without getting charged for the resources until ready to use them.
