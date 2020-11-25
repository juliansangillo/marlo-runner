pipeline {
  agent none
  stages {
    stage('Initialize') {
      agent {
        node {
          label 'jenkins-agent'
        }

      }
      steps {
        echo "Initialize starting on Node ${env.NODE_NAME} ..."
        dir(path: '/tmp/repository') {
          checkout scm
        }

        script {
          env.LICENSE=""
          env.PROJECT_PATH="./Marlo Runner"
          env.BUILD_NAME="MarloRunner"
          env.VERSION="1.0.0"
          env.PLATFORMS=["StandaloneLinux64","StandaloneWindows64","StandaloneWindows"]
          env.BUILD_EXTENSIONS=[StandaloneWindows64:"exe",StandaloneWindows:"exe",StandaloneOSX:"app",Android:"apk"]
          env.IS_DEVELOPMENT_BUILD=false
        }

        echo 'Initialize complete'
      }
    }

    stage('Preparing for build') {
      agent {
        node {
          label 'jenkins-agent'
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
        sh 'ls /tmp/repository'
        sh 'docker pull sicklecell29/unity3d:latest'
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
          parallelize 'jenkins-agent', env.PLATFORMS, {
            PLATFORM ->
            echo "Build starting on Node ${env.NODE_NAME} ..."

            def extension = env.BUILD_EXTENSIONS[PLATFORM]
            def fileExtensionArg = ""
            if(extension) {
              fileExtensionArg = "-fileExtension ${extension}"
            }

            def developmentBuildFlag = ""
            if(env.IS_DEVELOPMENT_BUILD) {
              developmentBuildFlag = "-developmentBuild"
            }

            sh """
            docker container run \
            --mount type=bind,source=/tmp/repository,target=/var/unity-home \
            sicklecell29/unity3d:latest \
            -license "${env.LICENSE}" \
            -projectPath "${env.PROJECT_PATH}" \
            -platform ${PLATFORM} \
            "$fileExtensionArg" \
            -buildName "${env.BUILD_NAME}" \
            -version ${env.VERSION} \
            "${developmentBuildFlag}"
            """

            sh "ls /tmp/repository/bin/${PLATFORM}"

            echo "Build complete"
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
    parallelsAlwaysFailFast()
  }
}