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
          env.PLATFORMS="StandaloneWindows64 StandaloneWindows32"
          env.IS_DEVELOPMENT_BUILD=false
        }

        checkout scm
        sh 'tar -czf /tmp/$BUILD_TAG.tar.gz .'
        googleStorageUpload(credentialsId: 'unity-firebuild', bucket: "gs://${env.TMP_BUCKET}", pattern: "/tmp/${env.BUILD_TAG}.tar.gz")
        echo 'Initialize complete'
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

            println "Build started on Node ${env.NODE_NAME} ..."
            sh 'ls'

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
  }
}