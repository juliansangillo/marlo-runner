pipeline {
  agent {
    node {
      label 'jenkins-agent'
    }

  }
  stages {
    stage('Initialize') {
      agent any
      steps {
        echo "Initialize starting on Node ${env.NODE_NAME} ..."
        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS="StandaloneOSX StandaloneWindows32"
          env.IS_DEVELOPMENT_BUILD=false
        }

        echo 'Initialize complete'
      }
    }

    stage('Preparing for build') {
      agent any
      when {
        beforeAgent true
        expression {
          return env.PLATFORMS.replaceAll("\\s","") != ""
        }

      }
      steps {
        echo "Preparing for build starting on Node ${env.NODE_NAME} ..."
        sh 'ls'
        echo 'Preparing for build complete'
      }
    }

    stage('Build') {
      agent any
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
            sh 'ls'
            echo "Build complete"

          }
        }

      }
    }

    stage('Test Build') {
      when {
        beforeAgent true
        expression {
          return null
        }

      }
      steps {
        script {
          parallelize 'jenkins-agent', env.PLATFORMS.split(' '), {

            echo "Build starting on Node ${env.NODE_NAME} ..."
            env.REPO_URL = scm.getUserRemoteConfigs()[0].getUrl()
            sh 'git clone "$REPO_URL" . -b $BRANCH_NAME'
            sh 'ls'
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
    parallelsAlwaysFailFast()
  }
}