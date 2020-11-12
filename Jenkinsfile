pipeline {
  agent any
  stages {
    stage('Initialize') {
      agent any
      steps {
        echo 'Initialize starting .....'
        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS="StandaloneLinux64 StandaloneWindows64"
          env.IS_DEVELOPMENT_BUILD=false
        }

        echo 'Initialize complete'
      }
    }

    stage('Build') {
      parallel {
        stage('Build Linux x64') {
          agent any
          when {
            beforeAgent true
            expression {
              def platform_list = env.PLATFORMS.split(' ')
              return platform_list.contains("${env.LINUX64_PLATFORM}")
            }

          }
          steps {
            echo 'Hello'
            sh 'echo "$(hostname)";'
          }
        }

        stage('Build Windows x64') {
          agent any
          when {
            beforeAgent true
            expression {
              def platform_list = env.PLATFORMS.split(' ')
              return platform_list.contains("${env.WIN64_PLATFORM}")
            }

          }
          steps {
            echo 'Hello'
            sh 'echo "$(hostname)";'
          }
        }

      }
    }

    stage('Matrix') {
      agent any
      steps {
        script {
          def axisValues = env.PLATFORMS.split(' ')
          def tasks = [:]
          for(int i=0; i< axisValues.size(); i++) {
            def axisValue = axisValues[i]
            tasks["${axisValue}"] = {
              node(axisValue) {
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