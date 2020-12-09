pipeline {
  agent none
  stages {
    stage('Initialize') {
      agent {
        node {
          label "${env.AGENT_PREFIX}"
        }

      }
      steps {
        echo "Initialized on node: ${env.NODE_NAME}"
        dir(path: "${env.LOCAL_REPOSITORY}") {
          checkout scm
        }

        script {
          env.PROJECT_PATH = './Marlo Runner'
          env.BUILD_NAME = 'MarloRunner'
          env.PLATFORMS = 'StandaloneLinux64 StandaloneWindows64'
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
          label "${env.AGENT_PREFIX}"
        }

      }
      when {
        beforeAgent true
        expression {
          return env.PLATFORMS.replaceAll("\\s","") != ""
        }

      }
      steps {
        dir(path: "${env.LOCAL_REPOSITORY}") {
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

        script {
          cloud.login env.GOOGLE_PROJECT env.JENKINS_CREDENTIALS_ID
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
          parallelize env.AGENT_PREFIX, env.PLATFORMS.split(' '), {
            PLATFORM ->
            try {
              sh 'cp -al "$LOCAL_REPOSITORY/$PROJECT_PATH" .'
              echo 'Hard linked project to workspace'
              sh 'ls -l "$PROJECT_PATH/Assets"'
              sh 'cat "$LOCAL_REPOSITORY/$PROJECT_PATH/ProjectSettings/ProjectSettings.asset"'

              cloud.uncache "//${env.CACHE_BUCKET}/${env.JOB_NAME}/${PLATFORM}" "${env.PROJECT_PATH}"

              echo 'Starting Unity build ...'
              unity.build env.WORKSPACE, env.UNITY_DOCKER_IMG, env.PROJECT_PATH, PLATFORM, env.FILE_EXTENSIONS, env.BUILD_NAME, env.VERSION, env.IS_DEVELOPMENT_BUILD
              echo 'Unity build complete'

              cloud.cache "//${env.CACHE_BUCKET}/${env.JOB_NAME}/${PLATFORM}" "${env.PROJECT_PATH}" 'Library'

              sh 'sudo chown -R jenkins:jenkins bin'

              dir("bin/${PLATFORM}") {
                sh "ls ${env.BUILD_NAME}"
                sh "mkdir -p ${env.LOCAL_REPOSITORY}/bin"
                sh "zip -r -m ${env.LOCAL_REPOSITORY}/bin/${env.BUILD_NAME}-${PLATFORM}.zip ${env.BUILD_NAME}"
              }
            }
            finally {
              sh 'sudo rm -rf ./**'
            }
          }
        }

      }
    }

    stage('Publish') {
      agent {
        node {
          label "${env.AGENT_PREFIX}"
        }

      }
      when {
        beforeAgent true
        expression {
          return env.PLATFORMS.replaceAll("\\s","") != ""
        }

      }
      steps {
        dir(path: "${env.LOCAL_REPOSITORY}") {
          script {
            semantic.release "${env.GITHUB_CREDENTIALS_ID}"
          }

        }

      }
    }

  }
  environment {
    GOOGLE_PROJECT = 'unity-firebuild'
    CACHE_BUCKET = 'unity-firebuild-cache'
    UNITY_DOCKER_IMG = 'sicklecell29/unity3d:latest'
    AGENT_PREFIX = 'jenkins-agent'
    LOCAL_REPOSITORY = '/tmp/repository'
    JENKINS_CREDENTIALS_ID = 'jenkins-sa'
    GITHUB_CREDENTIALS_ID = 'github-credentials'
  }
  post {
    always {
      node(env.AGENT_PREFIX) {
        sh 'rm -rf $LOCAL_REPOSITORY/bin/**'
      }

    }

  }
  options {
    skipDefaultCheckout(true)
    parallelsAlwaysFailFast()
  }
}