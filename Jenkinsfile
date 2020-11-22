pipeline {
  agent none
  stages {
    stage('Initialize') {
      agent {
        node {
          label 'jenkins-agent-0'
        }

      }
      steps {
        echo "Initialize starting on Node ${env.NODE_NAME} ..."
        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS="StandaloneWindows32"
          env.IS_DEVELOPMENT_BUILD=false
        }

        echo 'Initialize complete'
      }
    }

    stage('Preparing for build') {
      agent {
        node {
          label 'jenkins-agent-0'
        }

      }
      when {
        beforeAgent true
        expression {
          return null
        }

      }
      steps {
        echo "Preparing for build starting on Node ${env.NODE_NAME} ..."
        checkout scm
        sh 'tar -czf /tmp/$BUILD_TAG.tar.gz .'
        dir(path: '/tmp') {
          googleStorageUpload(credentialsId: 'unity-firebuild', bucket: "gs://${env.TMP_BUCKET}", pattern: "${env.BUILD_TAG}.tar.gz")
        }

        echo 'Preparing for build complete'
      }
    }

    stage('Build') {
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
            lock('google-storage-repo-archive') {
              googleStorageDownload(bucketUri: "gs://${env.TMP_BUCKET}/${env.BUILD_TAG}.tar.gz", localDirectory: '/tmp', credentialsId: 'unity-firebuild')
            }
            sh 'tar -xf /tmp/$BUILD_TAG.tar.gz'
            sh 'ls'
            echo "Build complete"

          }
        }

      }
    }

    stage('Test Build') {
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
    skipDefaultCheckout(true)
    parallelsAlwaysFailFast()
  }
}