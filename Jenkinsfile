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
          env.PROJECT_PATH = './Marlo Runner'
          env.BUILD_NAME = 'MarloRunner'
          env.PLATFORMS = 'StandaloneLinux64'
          env.FILE_EXTENSIONS = 'StandaloneWindows64:exe StandaloneWindows:exe StandaloneOSX:app Android:apk'
          env.IS_DEVELOPMENT_BUILD = 'false'

          env.CHANGELOG_FILE_NAME = 'CHANGELOG.md'
          env.CHANGELOG_TITLE = 'CHANGELOG'

          env.MAPPING_PROD_BRANCH = 'master'
          env.MAPPING_PROD_PRERELEASE = 'false'
          env.MAPPING_TEST_BRANCH = 'beta'
          env.MAPPING_TEST_PRERELEASE = 'true'
          env.MAPPING_DEV_BRANCH = 'alpha'
          env.MAPPING_DEV_PRERELEASE = 'true'
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
        script {
          unity.init 'sicklecell29/unity3d:latest'
        }

        dir(path: '/tmp/repository') {
          script {
            semantic.init env.MAPPING_PROD_BRANCH, env.MAPPING_TEST_BRANCH, env.MAPPING_DEV_BRANCH, env.MAPPING_PROD_PRERELEASE, env.MAPPING_TEST_PRERELEASE, env.MAPPING_DEV_PRERELEASE, env.CHANGELOG_FILE_NAME, env.CHANGELOG_TITLE
          }

          script {
            withCredentials([usernameColonPassword(credentialsId: 'github-credentials', variable: 'GITHUB_CREDS')]) {
              env.VERSION = semantic.version GITHUB_CREDS.split(':')[1]
            }
            sh 'printenv'
          }

        }

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
            PLATFORM ->
            echo "Build starting on Node ${env.NODE_NAME} ..."

            sh 'cp -al "/tmp/repository/$PROJECT_PATH" .'
            sh 'ls -l "$PROJECT_PATH"'

            unity.build env.WORKSPACE, 'sicklecell29/unity3d:latest', env.PROJECT_PATH, PLATFORM, env.FILE_EXTENSIONS, env.BUILD_NAME, env.VERSION, env.IS_DEVELOPMENT_BUILD
            sh "ls bin/${PLATFORM}/${env.BUILD_NAME}"
            sh 'cat "/tmp/repository/$PROJECT_PATH/ProjectSettings/ProjectSettings.asset"'

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