pipeline {
  agent any
  stages {
    stage('Initialize') {
      agent any
      steps {
        echo 'Initialize starting .....'
        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS="StandaloneLinux64 StandaloneWindows64"
          env.IS_DEVELOPMENT_BUILD=false
        }

        sh """echo "Ingest config file";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                echo "${env.LICENSE}";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                echo "${env.PROJECT_PATH}";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                echo "${env.BUILD_NAME}";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                echo "${env.VERSION}";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                echo "${env.PLATFORMS}";
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                echo "${env.IS_DEVELOPMENT_BUILD}";"""
        echo 'Initialize complete'
      }
    }

    stage('Build') {
      agent {
        node {
          label 'jenkins-agent'
        }

      }
      when {
        beforeAgent true
        expression {
          def platform_list = env.PLATFORMS.split(' ')
          return platform_list.contains("${env.PLATFORM}")
        }

      }
      steps {
        echo 'Hello'
        sh 'echo "$(hostname)";'
      }
    }

  }
  environment {
    BUILD_BUCKET = 'unity-firebuild-artifacts'
    PLATFORM = 'StandaloneLinux64'
  }
  options {
    skipDefaultCheckout(true)
  }
}