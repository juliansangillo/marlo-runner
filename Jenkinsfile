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
          env.PLATFORMS="StandaloneOSX StandaloneWindows32"
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
          return env.PLATFORMS.replaceAll("\\s","") != ""
        }

      }
      steps {
        echo "Preparing for build starting on Node ${env.NODE_NAME} ..."
        withCredentials(bindings: [[$class: 'MultiBinding', credentialsId: 'unity-firebuild', variable: 'SA_KEY']]) {
          sh "gcloud auth activate-service-account --key-file=${SA_KEY}"
        }

        sh 'gcloud compute disks create jenkins-shared-workspace --size=50GB --type=pd-standard'
        sh 'gcloud compute instances attach-disk $NODE_NAME --disk=jenkins-shared-workspace --device-name=jsw'
        sh 'mkfs.ext4 -m 0 -E lazy_itable_init=0,lazy_journal_init=0,discard /dev/jsw'
        sh 'mount -o discard,defaults /dev/jsw .'
        checkout scm
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
            withCredentials([[$class: 'MultiBinding', credentialsId: 'unity-firebuild', variable: 'SA_KEY']]) {
              sh "gcloud auth activate-service-account --key-file=${SA_KEY}"
            }
            sh 'gcloud compute instances attach-disk $NODE_NAME --disk=jenkins-shared-workspace --device-name=jsw'
            sh 'mount -o discard,defaults /dev/jsw .'
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
    skipDefaultCheckout(true)
    parallelsAlwaysFailFast()
  }
}