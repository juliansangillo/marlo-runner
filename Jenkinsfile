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
          script {
            def datas = loadYaml "${env.CONFIG_FILE}"

            echo datas.project-path

            env.PROJECT_PATH = datas.project-path

            echo "${env.PROJECT_PATH}"
          }

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
          cloud.login env.GOOGLE_PROJECT, env.JENKINS_CREDENTIALS_ID
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
              sh (
                script: 'cp -al "$LOCAL_REPOSITORY/$PROJECT_PATH" .',
                label: 'Hard link project to workspace'
              )
              sh (
                script: 'ls -l "$PROJECT_PATH/Assets"',
                label: 'List project assets'
              )
              sh (
                script: 'cat "$LOCAL_REPOSITORY/$PROJECT_PATH/ProjectSettings/ProjectSettings.asset"',
                label: 'Print project settings'
              )

              cloud.uncache "//${env.CACHE_BUCKET}/${env.JOB_NAME}/${PLATFORM}", "${env.PROJECT_PATH}"

              unity.build env.WORKSPACE, env.UNITY_DOCKER_IMG, env.PROJECT_PATH, PLATFORM, env.FILE_EXTENSIONS, env.BUILD_NAME, env.VERSION, env.IS_DEVELOPMENT_BUILD

              cloud.cache "//${env.CACHE_BUCKET}/${env.JOB_NAME}/${PLATFORM}", "${env.PROJECT_PATH}", 'Library'

              sh 'sudo chown -R jenkins:jenkins bin'

              dir("bin/${PLATFORM}") {
                sh (
                  script: "ls ${env.BUILD_NAME}",
                  label: 'List artifacts'
                )
                sh (
                  script: "mkdir -p ${env.LOCAL_REPOSITORY}/bin",
                  label: 'Create repository bin if doesnt exist'
                )
                sh (
                  script: "zip -r -m ${env.LOCAL_REPOSITORY}/bin/${env.BUILD_NAME}-${PLATFORM}.zip ${env.BUILD_NAME}",
                  label: 'Archive artifacts'
                )
              }
            }
            finally {
              sh (
                script: 'sudo rm -rf ./**',
                label: 'Post workspace cleanup'
              )
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
    CONFIG_FILE = 'unityci.yml'
  }
  post {
    always {
      node(env.AGENT_PREFIX) {
        sh(script: 'rm -rf $LOCAL_REPOSITORY/bin/**', label: 'Post repository cleanup')
      }

    }

  }
  options {
    skipDefaultCheckout(true)
    parallelsAlwaysFailFast()
  }
}