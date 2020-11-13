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
          env.PLATFORMS="StandaloneLinux64 StandaloneWindows64"
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

    stage('Test') {
      parallel {
        stage('Test 0') {
          agent {
            node {
              label 'jenkins-agent-0'
            }

          }
          steps {
            script {
              println "Node=${NODE_NAME}"
            }

          }
        }

        stage('Test 1') {
          agent {
            node {
              label 'jenkins-agent-1'
            }

          }
          steps {
            script {
              println "Node=${NODE_NAME}"
            }

          }
        }

        stage('Test 2') {
          agent {
            node {
              label 'jenkins-agent-2'
            }

          }
          steps {
            script {
              println "Node=${NODE_NAME}"
            }

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