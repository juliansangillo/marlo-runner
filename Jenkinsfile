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
          env.PLATFORMS="StandaloneLinux64"
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
          def prefix = 'jenkins-agent'
          def axisValues = env.PLATFORMS.split(' ')
          def tasks = [:]
          for(int i = 0; i < axisValues.size(); i++) {
            def axisValue = axisValues[i]
            def label = prefix + '-' + i
            tasks[axisValue] = {
              stage(axisValue) {
                node(label) {
                  println "${label}"
                  println "Node=${env.NODE_NAME}"
                }
              }
            }
          }

          parallel tasks
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