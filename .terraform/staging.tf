locals {
  staging_env_vars = jsondecode(data.aws_secretsmanager_secret_version.customcads_staging_env_variables_version.secret_string)

  staging_jwt            = local.staging_env_vars["JwtOptions"]
  staging_jwt_issuer     = local.staging_jwt["Issuer"]
  staging_jwt_audience   = local.staging_jwt["Audience"]
  staging_jwt_secret_key = local.staging_jwt["SecretKey"]

  staging_payment                      = local.staging_env_vars["Payment"]
  staging_payment_secret_key           = local.staging_payment["SecretKey"]
  staging_payment_publishable_key      = local.staging_payment["PublishableKey"]
  staging_payment_test_secret_key      = local.staging_payment["TestSecretKey"]
  staging_payment_test_publishable_key = local.staging_payment["TestPublishableKey"]

  staging_email          = local.staging_env_vars["Email"]
  staging_email_server   = local.staging_email["Server"]
  staging_email_port     = local.staging_email["Port"]
  staging_email_from     = local.staging_email["From"]
  staging_email_password = local.staging_email["Password"]

  staging_storage            = local.staging_env_vars["Storage"]
  staging_storage_access_key = local.staging_storage["AccessKey"]
  staging_storage_secret_key = local.staging_storage["SecretKey"]

  staging_delivery          = local.staging_env_vars["Delivery"]
  staging_delivery_username = local.staging_delivery["Username"]
  staging_delivery_password = local.staging_delivery["Password"]

  staging_urls           = local.staging_env_vars["ClientURLs"]
  staging_urls_all       = local.staging_urls["All"]
  staging_urls_preferred = local.staging_urls["Preferred"]

  staging_cookie        = local.staging_env_vars["Cookie"]
  staging_cookie_domain = local.staging_urls["Domain"]
}

# Staging Environment
resource "aws_elastic_beanstalk_environment" "customcads_env_staging" {
  application         = "CustomCADs"
  cname_prefix        = "staging-customcads"
  description         = "CustomCADs Staging environment"
  name                = "CustomCADs-stag"
  solution_stack_name = "64bit Amazon Linux 2023 v4.5.0 running Docker"
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
    value     = "Host=${aws_db_instance.customcads_staging_database.endpoint};Database=${aws_db_instance.customcads_staging_database.db_name};Username=${local.staging_db_username};Password=${local.staging_db_password}"
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
    value     = local.staging_delivery_password
  }
  setting {
    name      = "Delivery__Username"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_delivery_username
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
    value     = local.staging_email_server
  }
  setting {
    name      = "Email__Port"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_email_port
  }
  setting {
    name      = "Email__From"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_email_from
  }
  setting {
    name      = "Email__Password"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_email_password
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
    value     = local.staging_jwt_audience
  }
  setting {
    name      = "JwtOptions__Issuer"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_jwt_issuer
  }
  setting {
    name      = "JwtOptions__SecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_jwt_secret_key
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
    value     = local.staging_payment_publishable_key
  }
  setting {
    name      = "Payment__SecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_payment_secret_key
  }
  setting {
    name      = "Payment__TestPublishableKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_payment_test_publishable_key
  }
  setting {
    name      = "Payment__TestSecretKey"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_payment_test_secret_key
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
    value     = local.staging_storage_access_key
  }
  setting {
    name      = "Storage__BucketName"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = aws_s3_bucket.customcads_staging_bucket.bucket
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
    value     = local.staging_storage_secret_key
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
    value     = local.staging_urls_all
  }
  setting {
    name      = "ClientURLs__Preferred"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_urls_preferred
  }
  setting {
    name      = "Cookie__Domain"
    namespace = "aws:elasticbeanstalk:application:environment"
    resource  = null
    value     = local.staging_cookie_domain
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
