locals {
  jwt_issuer     = local.jwt["Issuer"]
  jwt_audience   = local.jwt["Audience"]
  jwt_secret_key = local.jwt["SecretKey"]

  payment_secret_key           = local.payment["SecretKey"]
  payment_publishable_key      = local.payment["PublishableKey"]
  payment_test_secret_key      = local.payment["TestSecretKey"]
  payment_test_publishable_key = local.payment["TestPublishableKey"]

  email_password = local.email["Password"]
  email_port     = local.email["Port"]

  storage_access_key = local.storage["AccessKey"]
  storage_secret_key = local.storage["SecretKey"]

  delivery_username = local.delivery["Username"]
  delivery_password = local.delivery["Password"]

  urls_client = local.urls["Client"]
  urls_server = local.urls["Server"]
}

# Production Environment
resource "aws_elastic_beanstalk_environment" "customcads_env_prod" {
  application         = "CustomCADs"
  cname_prefix        = "customcads"
  description         = "CustomCADs Production environment"
  name                = "CustomCADs-prod"
  solution_stack_name = "64bit Amazon Linux 2023 v4.4.1 running Docker"
  tier                = "WebServer"
  version_label       = "latest"

  setting {
    name      = "Automatically Terminate Unhealthy Instances"
    namespace = "aws:elasticbeanstalk:monitoring"
    resource  = null
    value     = "true"
  }

  setting {
    name      = "ConfigDocument"
    namespace = "aws:elasticbeanstalk:healthreporting:system"
    resource  = null
    value = jsonencode(
      {
        CloudWatchMetrics = {
          Environment = {}
          Instance    = {}
        }
        Rules = {
          Environment = {
            Application = {
              ApplicationRequests4xx = {
                Enabled = true
              }
            }
            ELB = {
              ELBRequests4xx = {
                Enabled = true
              }
            }
          }
        }
        Version = 1
      }
    )
  }
  setting {
    name      = "ConnectionStrings__ApplicationConnection"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = "Host=${aws_db_instance.customcads_database.endpoint};Database=${aws_db_instance.customcads_database.db_name};Username=${local.db_username};Password=${local.db_password}"
  }
  setting {
    name      = "DefaultSSHPort"
    namespace = "aws:elasticbeanstalk:control"
    resource  = null
    value     = "22"
  }
  setting {
    name      = "DeleteOnTerminate"
    namespace = "aws:elasticbeanstalk:cloudwatch:logs"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "DeleteOnTerminate"
    namespace = "aws:elasticbeanstalk:cloudwatch:logs:health"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "Delivery__Password"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.delivery_password
  }
  setting {
    name      = "Delivery__Username"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.delivery_username
  }
  setting {
    name      = "DeploymentPolicy"
    namespace = "aws:elasticbeanstalk:command"
    resource  = null
    value     = "AllAtOnce"
  }
  setting {
    name      = "DisableIMDSv1"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "EC2KeyName"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = aws_key_pair.customcads_key_pair.key_name
  }
  setting {
    name      = "ELBScheme"
    namespace = "aws:ec2:vpc"
    resource  = null
    value     = "public"
  }
  setting {
    name      = "ELBSubnets"
    namespace = "aws:ec2:vpc"
    resource  = null
    value     = aws_subnet.customcads_subnet_public2_b.id
  }
  setting {
    name      = "Email__Password"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.email_password
  }
  setting {
    name      = "Email__Port"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.email_port
  }
  setting {
    name      = "EnhancedHealthAuthEnabled"
    namespace = "aws:elasticbeanstalk:healthreporting:system"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "EnvironmentType"
    namespace = "aws:elasticbeanstalk:environment"
    resource  = null
    value     = "SingleInstance"
  }
  setting {
    name      = "HealthStreamingEnabled"
    namespace = "aws:elasticbeanstalk:cloudwatch:logs:health"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "HooksPkgUrl"
    namespace = "aws:cloudformation:template:parameter"
    resource  = null
    value     = "https://elasticbeanstalk-platform-assets-us-east-1.s3.amazonaws.com/stalks/eb_docker_amazon_linux_2023_1.0.155.0_20241119035555/lib/hooks.tar.gz"
  }
  setting {
    name      = "IamInstanceProfile"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = aws_iam_instance_profile.customcads_instance_profile.name
  }
  setting {
    name      = "InstanceRefreshEnabled"
    namespace = "aws:elasticbeanstalk:managedactions:platformupdate"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "InstanceType"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = "t3.micro"
  }
  setting {
    name      = "InstanceTypes"
    namespace = "aws:ec2:instances"
    resource  = null
    value     = "t3.micro, t3.small"
  }
  setting {
    name      = "JwtSettings__Audience"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.jwt_audience
  }
  setting {
    name      = "JwtSettings__Issuer"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.jwt_issuer
  }
  setting {
    name      = "JwtSettings__SecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.jwt_secret_key
  }
  setting {
    name      = "LaunchType"
    namespace = "aws:elasticbeanstalk:control"
    resource  = null
    value     = "Migration"
  }
  setting {
    name      = "ManagedActionsEnabled"
    namespace = "aws:elasticbeanstalk:managedactions"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "MaxSize"
    namespace = "aws:autoscaling:asg"
    resource  = null
    value     = "1"
  }
  setting {
    name      = "MinSize"
    namespace = "aws:autoscaling:asg"
    resource  = null
    value     = "1"
  }
  setting {
    name      = "Notification Endpoint"
    namespace = "aws:elasticbeanstalk:sns:topics"
    resource  = null
    value     = "customcads414@gmail.com"
  }
  setting {
    name      = "Notification Protocol"
    namespace = "aws:elasticbeanstalk:sns:topics"
    resource  = null
    value     = "email"
  }
  setting {
    name      = "Notification Topic ARN"
    namespace = "aws:elasticbeanstalk:sns:topics"
    resource  = null
    value     = aws_sns_topic.customcads_notification.arn
  }
  setting {
    name      = "Payment__PublishableKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.payment_publishable_key
  }
  setting {
    name      = "Payment__SecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.payment_secret_key
  }
  setting {
    name      = "Payment__TestPublishableKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.payment_test_publishable_key
  }
  setting {
    name      = "Payment__TestSecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.payment_test_secret_key
  }
  setting {
    name      = "PreferredStartTime"
    namespace = "aws:elasticbeanstalk:managedactions"
    resource  = null
    value     = "SUN:11:00"
  }
  setting {
    name      = "ProxyServer"
    namespace = "aws:elasticbeanstalk:environment:proxy"
    resource  = null
    value     = "nginx"
  }
  setting {
    name      = "RetentionInDays"
    namespace = "aws:elasticbeanstalk:cloudwatch:logs"
    resource  = null
    value     = "3"
  }
  setting {
    name      = "RetentionInDays"
    namespace = "aws:elasticbeanstalk:cloudwatch:logs:health"
    resource  = null
    value     = "3"
  }
  setting {
    name      = "RollbackLaunchOnFailure"
    namespace = "aws:elasticbeanstalk:control"
    resource  = null
    value     = "false"
  }
  setting {
    name      = "RollingUpdateEnabled"
    namespace = "aws:autoscaling:updatepolicy:rollingupdate"
    resource  = null
    value     = "false"
  }
  setting {
    name      = "RollingUpdateType"
    namespace = "aws:autoscaling:updatepolicy:rollingupdate"
    resource  = null
    value     = "Immutable"
  }
  setting {
    name      = "RootVolumeIOPS"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = "3000"
  }
  setting {
    name      = "RootVolumeSize"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = "10"
  }
  setting {
    name      = "RootVolumeThroughput"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = "125"
  }
  setting {
    name      = "RootVolumeType"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = "gp3"
  }
  setting {
    name      = "SSHSourceRestriction"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = "tcp,22,22,0.0.0.0/0"
  }
  setting {
    name      = "SecurityGroups"
    namespace = "aws:autoscaling:launchconfiguration"
    resource  = null
    value     = aws_security_group.customcads_app_security_group.id
  }
  setting {
    name      = "ServiceRole"
    namespace = "aws:elasticbeanstalk:environment"
    resource  = null
    value     = aws_iam_role.customcads_eb_service_role.arn
  }
  setting {
    name      = "ServiceRoleForManagedUpdates"
    namespace = "aws:elasticbeanstalk:managedactions"
    resource  = null
    value     = aws_iam_role.customcads_eb_service_role.arn
  }
  setting {
    name      = "Storage__AccessKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.storage_access_key
  }
  setting {
    name      = "Storage__BucketName"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = aws_s3_bucket.customcads_bucket.bucket
  }
  setting {
    name      = "Storage__Region"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = var.region
  }
  setting {
    name      = "Storage__SecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.storage_secret_key
  }
  setting {
    name      = "StreamLogs"
    namespace = "aws:elasticbeanstalk:cloudwatch:logs"
    resource  = null
    value     = "true"
  }
  setting {
    name      = "Subnets"
    namespace = "aws:ec2:vpc"
    resource  = null
    value     = aws_subnet.customcads_subnet_public2_b.id
  }
  setting {
    name      = "SupportedArchitectures"
    namespace = "aws:ec2:instances"
    resource  = null
    value     = "x86_64"
  }
  setting {
    name      = "SystemType"
    namespace = "aws:elasticbeanstalk:healthreporting:system"
    resource  = null
    value     = "enhanced"
  }
  setting {
    name      = "URLs__Client"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.urls_client
  }
  setting {
    name      = "URLs__Server_"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.urls_server
  }
  setting {
    name      = "UpdateLevel"
    namespace = "aws:elasticbeanstalk:managedactions:platformupdate"
    resource  = null
    value     = "minor"
  }
  setting {
    name      = "VPCId"
    namespace = "aws:ec2:vpc"
    resource  = null
    value     = aws_vpc.customcads_vpc.id
  }
}
