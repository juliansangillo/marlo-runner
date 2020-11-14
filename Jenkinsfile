pipeline {
  agent none
  stages {
    stage('Initialize') {
      steps {
        echo 'Initialize starting .....'
        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS=""
          env.IS_DEVELOPMENT_BUILD=false
        }

        echo 'Initialize complete'
      }
    }

    stage('Build') {
      steps {
        script {
          def prefix = 'jenkins-agent'
          def axisValues = env.PLATFORMS.split(' ')

          if(axisValues.size() > 0) {
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
          else {
            Utils.markStageSkippedForConditional('Build')
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