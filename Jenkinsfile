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
        echo "Initialized on node: ${env.NODE_NAME}"
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
        dir(path: '/tmp/repository') {
          script {
            semantic.init env.MAPPING_PROD_BRANCH, env.MAPPING_TEST_BRANCH, env.MAPPING_DEV_BRANCH, env.MAPPING_PROD_PRERELEASE, env.MAPPING_TEST_PRERELEASE, env.MAPPING_DEV_PRERELEASE, env.CHANGELOG_FILE_NAME, env.CHANGELOG_TITLE
          }

          script {
            env.VERSION = semantic.version "${env.GITHUB_CREDENTIALS_ID}"
          }

          echo "VERSION=${env.VERSION}"
        }

        script {
          unity.init env.UNITY_DOCKER_IMG
        }

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
            sh 'cp -al "/tmp/repository/$PROJECT_PATH" .'
            echo 'Hard linked project to workspace'
            sh 'ls -l "$PROJECT_PATH/Assets"'
            sh 'cat "/tmp/repository/$PROJECT_PATH/ProjectSettings/ProjectSettings.asset"'

            echo 'Starting Unity build ...'
            unity.build env.WORKSPACE, env.UNITY_DOCKER_IMG, env.PROJECT_PATH, PLATFORM, env.FILE_EXTENSIONS, env.BUILD_NAME, env.VERSION, env.IS_DEVELOPMENT_BUILD
            echo 'Unity build complete'

            sh 'ls -l'
            sh 'ls -l bin'

            dir("bin/${PLATFORM}") {
              sh 'ls -l'
              sh "ls ${env.BUILD_NAME}"
              sh 'mkdir -p /tmp/repository/bin'
              sh "sudo zip -r -m /tmp/repository/bin/${env.BUILD_NAME}-${PLATFORM}.zip ${env.BUILD_NAME}"
            }
          }
        }

      }
    }

    stage('Publish') {
      steps {
        script {
          semantic.release "${env.GITHUB_CREDENTIALS_ID}"
        }

      }
    }

  }
  environment {
    BUILD_BUCKET = 'unity-firebuild-artifacts'
    UNITY_DOCKER_IMG = 'sicklecell29/unity3d:latest'
    GITHUB_CREDENTIALS_ID = 'github-credentials'
  }
  options {
    skipDefaultCheckout(true)
    parallelsAlwaysFailFast()
  }
}