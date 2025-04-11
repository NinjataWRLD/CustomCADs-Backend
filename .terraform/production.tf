locals {
  production_env_vars = jsondecode(data.aws_secretsmanager_secret_version.customcads_production_env_variables_version.secret_string)

  production_jwt            = local.production_env_vars["JwtOptions"]
  production_jwt_issuer     = local.production_jwt["Issuer"]
  production_jwt_audience   = local.production_jwt["Audience"]
  production_jwt_secret_key = local.production_jwt["SecretKey"]

  production_payment                      = local.production_env_vars["Payment"]
  production_payment_secret_key           = local.production_payment["SecretKey"]
  production_payment_publishable_key      = local.production_payment["PublishableKey"]
  production_payment_test_secret_key      = local.production_payment["TestSecretKey"]
  production_payment_test_publishable_key = local.production_payment["TestPublishableKey"]

  production_email          = local.production_env_vars["Email"]
  production_email_server   = local.production_email["Server"]
  production_email_port     = local.production_email["Port"]
  production_email_from     = local.production_email["From"]
  production_email_password = local.production_email["Password"]

  production_storage            = local.production_env_vars["Storage"]
  production_storage_access_key = local.production_storage["AccessKey"]
  production_storage_secret_key = local.production_storage["SecretKey"]

  production_delivery          = local.production_env_vars["Delivery"]
  production_delivery_username = local.production_delivery["Username"]
  production_delivery_password = local.production_delivery["Password"]

  production_urls           = local.production_env_vars["ClientURLs"]
  production_urls_all       = local.production_urls["All"]
  production_urls_preferred = local.production_urls["Preferred"]

  production_cookie        = local.production_env_vars["Cookie"]
  production_cookie_domain = local.production_cookie["Domain"]
}

# Production Environment
resource "aws_elastic_beanstalk_environment" "customcads_env_prod" {
  application         = "CustomCADs"
  cname_prefix        = "customcads"
  description         = "CustomCADs Production environment"
  name                = "CustomCADs-prod"
  solution_stack_name = "64bit Amazon Linux 2023 v4.5.0 running Docker"
  tier                = "WebServer"

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
    value     = "Host=${aws_db_instance.customcads_production_database.endpoint};Database=${aws_db_instance.customcads_production_database.db_name};Username=${local.production_db_username};Password=${local.production_db_password}"
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
    value     = local.production_delivery_password
  }
  setting {
    name      = "Delivery__Username"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_delivery_username
  }
  setting {
    name      = "DeploymentPolicy"
    namespace = "aws:elasticbeanstalk:command"
    resource  = null
    value     = "Immutable"
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
    name      = "Email__Server"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_email_server
  }
  setting {
    name      = "Email__Port"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_email_port
  }
  setting {
    name      = "Email__From"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_email_from
  }
  setting {
    name      = "Email__Password"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_email_password
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
    name      = "JwtOptions__Audience"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_jwt_audience
  }
  setting {
    name      = "JwtOptions__Issuer"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_jwt_issuer
  }
  setting {
    name      = "JwtOptions__SecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_jwt_secret_key
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
    value     = local.production_payment_publishable_key
  }
  setting {
    name      = "Payment__SecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_payment_secret_key
  }
  setting {
    name      = "Payment__TestPublishableKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_payment_test_publishable_key
  }
  setting {
    name      = "Payment__TestSecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_payment_test_secret_key
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
    value     = local.production_storage_access_key
  }
  setting {
    name      = "Storage__BucketName"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = aws_s3_bucket.customcads_production_bucket.bucket
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
    value     = local.production_storage_secret_key
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
    name      = "ClientURLs__All"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_urls_all
  }
  setting {
    name      = "ClientURLs__Preferred"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_urls_preferred
  }
  setting {
    name      = "Cookie__Domain"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.production_cookie_domain
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
