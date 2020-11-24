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
        withCredentials(bindings: [file(credentialsId:'jenkins-sa', variable: 'SA_KEY')]) {
          sh "gcloud auth activate-service-account --key-file=${SA_KEY}"
          sh 'ls ~/.config/gcloud/**/*'
          dir(path: '/home/jenkins') {
            sh 'ls -a'
            stash(name: 'jenkins-sa', includes: '.config/gcloud/**/*')
          }

        }

        sh 'gcloud compute disks create jenkins-shared-workspace --size=50GB --type=pd-standard --zone=us-east1-b'
        sh 'gcloud compute instances attach-disk $NODE_NAME --disk=jenkins-shared-workspace --zone=us-east1-b'
        sh 'sudo mkfs.ext4 -m 0 -E lazy_itable_init=0,lazy_journal_init=0,discard /dev/sdb'
        sh 'mkdir jenkins-shared-workspace'
        sh 'sudo mount -o discard,defaults,rw /dev/sdb jenkins-shared-workspace'
        sh 'sudo chown -R jenkins:jenkins jenkins-shared-workspace'
        dir(path: 'jenkins-shared-workspace') {
          checkout scm
        }

        sh 'sudo umount jenkins-shared-workspace'
        sh 'gcloud compute instances detach-disk $NODE_NAME --disk=jenkins-shared-workspace --zone=us-east1-b'
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
            dir('/home/jenkins') {
              unstash('jenkins-sa')
            }
            sh 'gcloud compute instances attach-disk $NODE_NAME --disk=jenkins-shared-workspace --zone=us-east1-b'
            sh 'mkdir jenkins-shared-workspace'
            sh 'sudo mount -o discard,defaults,rw /dev/sdb jenkins-shared-workspace'
            sh 'sudo chown -R jenkins:jenkins jenkins-shared-workspace'
            dir('jenkins-shared-workspace') {
              sh 'ls'
            }
            sh 'sudo umount jenkins-shared-workspace'
            sh 'gcloud compute instances detach-disk $NODE_NAME --disk=jenkins-shared-workspace --zone=us-east1-b'
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