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
          env.PLATFORMS="StandaloneLinux64 StandaloneWindows64 StandaloneWindows32 StandaloneOSX"
          env.IS_DEVELOPMENT_BUILD=false
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
          @Library('drg-jenkins-library@1.0') _

          parallelize 'jenkins-agent', env.PLATFORMS.split(' '), {

            println "${label}"
            println "Node=${env.NODE_NAME}"

          }
        }

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