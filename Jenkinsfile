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
          env.PLATFORMS="StandaloneLinux64 StandaloneWindows64 StandaloneOSX"
          env.IS_DEVELOPMENT_BUILD=false
        }

        echo 'Initialize complete'
      }
    }

    stage('Build') {
      steps {
        script {
          def label = 'jenkins-agent'
          def axisValues = env.PLATFORMS.split(' ')
          def tasks = [:]
          for(int i=0; i< axisValues.size(); i++) {
            def axisValue = axisValues[i]
            tasks["${axisValue}"] = {
              node("${label}") {
                println "Node=${env.NODE_NAME}"
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
    LINUX64_PLATFORM = 'StandaloneLinux64'
    WIN64_PLATFORM = 'StandaloneWindows64'
  }
  options {
    skipDefaultCheckout(true)
  }
}