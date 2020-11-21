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
        echo 'Initialize starting .....'
        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS="StandaloneWindows64 StandaloneWindows32"
          env.IS_DEVELOPMENT_BUILD=false
        }

        sh '''ls
ls ..'''
        script {
          working_dir = sh(returnStdout: true, script: 'basename $(pwd)').trim()
        }

        echo 'Initialize complete'
      }
    }

    stage('Build') {
      when {
        expression {
          return env.PLATFORMS.replaceAll("\\s","") != ""
        }

      }
      steps {
        script {
          parallelize 'jenkins-agent', env.PLATFORMS.split(' '), {

            println "Build started on Node ${env.NODE_NAME} ..."
            checkout scm
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