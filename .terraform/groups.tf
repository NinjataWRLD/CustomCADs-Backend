# IAM User
resource "aws_iam_user" "customcads_user" {
  name = "NinjataBG"
}

# IAM Groups
resource "aws_iam_group" "customcads_backend_group" {
  name = "Backend"
}

resource "aws_iam_group" "customcads_cloudfront_group" {
  name = "CloudFront"
}

resource "aws_iam_group" "customcads_iam_group" {
  name = "IdentityAccessManagement"
}

resource "aws_iam_group" "customcads_secretsmanager_group" {
  name = "SecretsManager"
}

resource "aws_iam_group" "customcads_sns_group" {
  name = "SimpleNotificationService"
}

# Attach Policies to Backend Group
resource "aws_iam_group_policy_attachment" "backend_ecr_access" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonEC2ContainerRegistryFullAccess"
}

resource "aws_iam_group_policy_attachment" "backend_ec2_access" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonEC2FullAccess"
}

resource "aws_iam_group_policy_attachment" "backend_rds_access" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonRDSFullAccess"
}

resource "aws_iam_group_policy_attachment" "backend_s3_access" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonS3FullAccess"
}

resource "aws_iam_group_policy_attachment" "backend_acm_access" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/AWSCertificateManagerFullAccess"
}

resource "aws_iam_group_policy_attachment" "backend_codedeploy_access" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/AWSCodeDeployFullAccess"
}

resource "aws_iam_group_policy_attachment" "backend_codedeploy_role" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/service-role/AWSCodeDeployRole"
}

resource "aws_iam_group_policy_attachment" "backend_eb_managed_updates" {
  group      = aws_iam_group.customcads_backend_group.name
  policy_arn = "arn:aws:iam::aws:policy/AWSElasticBeanstalkManagedUpdatesCustomerRolePolicy"
}

# Attach Policies to CloudFront Group
resource "aws_iam_group_policy_attachment" "cloudfront_access" {
  group      = aws_iam_group.customcads_cloudfront_group.name
  policy_arn = "arn:aws:iam::aws:policy/CloudFrontFullAccess"
}

# Attach Policies to IdentityAccessManagement Group
resource "aws_iam_group_policy_attachment" "iam_access" {
  group      = aws_iam_group.customcads_iam_group.name
  policy_arn = "arn:aws:iam::aws:policy/IAMFullAccess"
}

# Custom Policy for SecretsManager
resource "aws_iam_policy" "secretsmanager_read_policy" {
  name        = "SecretsManagerRead"
  description = "Read-only access to AWS Secrets Manager"
  policy = jsonencode({
    "Version" : "2012-10-17",
    "Statement" : [
      {
        "Sid" : "VisualEditor0",
        "Effect" : "Allow",
        "Action" : [
          "secretsmanager:GetResourcePolicy",
          "secretsmanager:GetSecretValue",
          "secretsmanager:DescribeSecret"
        ],
        "Resource" : "*"
      }
    ]
  })
}

# Attach Custom Policy to SecretsManager Group
resource "aws_iam_group_policy_attachment" "secretsmanager_access" {
  group      = aws_iam_group.customcads_secretsmanager_group.name
  policy_arn = aws_iam_policy.secretsmanager_read_policy.arn
}

# Attach Policies to SimpleNotificationService Group
resource "aws_iam_group_policy_attachment" "sns_access" {
  group      = aws_iam_group.customcads_sns_group.name
  policy_arn = "arn:aws:iam::aws:policy/AmazonSNSFullAccess"
}