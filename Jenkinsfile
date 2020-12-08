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

        script {
          echo 'Authenticating with google storage ...'
          withCredentials([file(credentialsId: "${env.JENKINS_CREDENTIALS_ID}", variable: 'SA_KEY')]) {
            sh "gcloud auth activate-service-account jenkins@unity-firebuild.iam.gserviceaccount.com --key-file=${SA_KEY} --project=${env.GOOGLE_PROJECT}"
          }
          echo 'success'
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

            echo 'Pulling from cache ...'
            def status = sh(
              script: "gsutil stat 'gs://${env.CACHE_BUCKET}/${env.JOB_NAME}/${PLATFORM}/'",
              returnStatus: true
            )
            if(status == 0) {
              sh "gsutil -m cp \"gs://${env.CACHE_BUCKET}/${env.JOB_NAME}/${PLATFORM}/**\" \"${env.PROJECT_PATH}\""
              echo 'Cache pulled successfully'
            }
            else {
              echo 'Cache objects don\'t exist. Skipping'
            }

            echo 'Starting Unity build ...'
            unity.build env.WORKSPACE, env.UNITY_DOCKER_IMG, env.PROJECT_PATH, PLATFORM, env.FILE_EXTENSIONS, env.BUILD_NAME, env.VERSION, env.IS_DEVELOPMENT_BUILD
            echo 'Unity build complete'

            echo 'Pushing to cache ...'
            sh "gsutil -m rsync -r \"${env.PROJECT_PATH}/Library\" \"gs://${env.CACHE_BUCKET}/${env.JOB_NAME}/${PLATFORM}/Library/\""
            echo 'Cache pushed successfully'

            sh 'sudo chown -R jenkins:jenkins bin'

            dir("bin/${PLATFORM}") {
              sh "ls ${env.BUILD_NAME}"
              sh 'mkdir -p /tmp/repository/bin'
              sh "zip -r -m /tmp/repository/bin/${env.BUILD_NAME}-${PLATFORM}.zip ${env.BUILD_NAME}"
            }

            post {
              always {
                sh "rm -rf ./**"
              }
            }
          }
        }

      }
    }

    stage('Publish') {
      agent {
        node {
          label 'jenkins-agent'
        }

      }
      post {
        always {
          sh 'rm -rf bin/**'
        }

      }
      steps {
        dir(path: '/tmp/repository') {
          script {
            semantic.release "${env.GITHUB_CREDENTIALS_ID}"
          }

        }

      }
    }

  }
  environment {
    CACHE_BUCKET = 'unity-firebuild-cache'
    UNITY_DOCKER_IMG = 'sicklecell29/unity3d:latest'
    GITHUB_CREDENTIALS_ID = 'github-credentials'
    JENKINS_CREDENTIALS_ID = 'jenkins-sa'
    GOOGLE_PROJECT = 'unity-firebuild'
  }
  options {
    skipDefaultCheckout(true)
    parallelsAlwaysFailFast()
  }
}