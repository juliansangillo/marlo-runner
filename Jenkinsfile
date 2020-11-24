pipeline {
  agent none
  stages {
    stage('Initialize') {
      agent {
        node {
          label 'jenkins-agent'
        }

      }
      steps {
        echo "Initialize starting on Node ${env.NODE_NAME} ..."
        dir(path: '/tmp/repositories') {
          checkout scm
        }

        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS="StandaloneLinux64 StandaloneWindows64 StandaloneWindows32"
          env.IS_DEVELOPMENT_BUILD=false
        }

        echo 'Initialize complete'
      }
    }

    stage('Preparing for build') {
      agent {
        node {
          label 'jenkins-agent'
        }

      }
      when {
        beforeAgent true
        expression {
          return env.PLATFORMS.replaceAll("\\s","") != ""
        }

      }
      steps {
        echo "Preparing for build starting on Node ${env.NODE_NAME} ..."
        sh 'ls /tmp/repositories'
        echo 'Preparing for build complete'
      }
    }

    stage('Build') {
      when {
        beforeAgent true
        expression {
          return env.PLATFORMS.replaceAll("\\s","") != ""
        }

      }
      steps {
        script {
          parallelize 'jenkins-agent', env.PLATFORMS.split(' '), {

            echo "Build starting on Node ${env.NODE_NAME} ..."
            sh 'touch $AXIS_NAME'
            sh 'ls'
            sh 'ls /tmp/repositories'
            echo "Build complete"

          }
        }

      }
    }

  }
  environment {
    BUILD_BUCKET = 'unity-firebuild-artifacts'
    TMP_BUCKET = 'unity-firebuild-tmp'
  }
  options {
    skipDefaultCheckout(true)
    parallelsAlwaysFailFast()
  }
}