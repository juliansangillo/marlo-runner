pipeline {
  agent {
    node {
      label 'jenkins-agent'
    }

  }
  stages {
    stage('Initialize') {
      steps {
        echo 'Initialize starting .....'
        sh '''echo "Ingest config file";

export env.LICENSE="";
export env.PROJECT_PATH="./Marlo Runner";
export env.BUILD_NAME=MarloRunner;
export env.VERSION=1.0.0;
export env.PLATFORMS="StandaloneLinux64";
export env.IS_DEVELOPMENT_BUILD=false;

echo "${env.LICENSE}" > /tmp/Unity.ulf;
echo "${env.PROJECT_PATH}" > /tmp/project-path;
echo "${env.BUILD_NAME}" > /tmp/build-name;
echo "${env.VERSION}" > /tmp/version;
echo "${env.PLATFORMS}" > /tmp/platforms;
echo "${env.IS_DEVELOPMENT_BUILD}" > /tmp/is-development-build;'''
        echo "gs://${env.TMP_BUCKET}/${env.JOB_NAME}/${env.BUILD_NUMBER}"
        echo 'Initialize complete'
      }
    }

    stage('Build') {
      steps {
        echo 'Build starting .....'
        sh '''echo "${env.LICENSE}";
echo "${env.PROJECT_PATH}";
echo "${env.BUILD_NAME}";
echo "${env.VERSION}";
echo "${env.PLATFORMS}";
echo "${env.IS_DEVELOPMENT_BUILD}";'''
        echo 'Build complete'
      }
    }

  }
  environment {
    BUILD_BUCKET = 'unity-firebuild-artifacts'
    TMP_BUCKET = 'unity-firebuild-tmp'
  }
  options {
    skipDefaultCheckout(true)
  }
}