pipeline {
  agent any
  stages {
    stage('Initialize') {
      agent {
        node {
          label 'jenkins-agent'
        }

      }
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
        expression {
          platforms = " ${env.PLATFORMS} "
          return platforms ==~ /.* StandaloneLinux64 .*/
        }

      }
      environment {
        PLATFORM = 'StandaloneLinux64'
      }
      steps {
        echo 'Hello'
        echo "${env.PLATFORM}"
        echo "${env.PLATFORMS}"
      }
    }

  }
  environment {
    BUILD_BUCKET = 'unity-firebuild-artifacts'
  }
  options {
    skipDefaultCheckout(true)
  }
}