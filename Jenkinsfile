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

export LICENSE="";
export PROJECT_PATH="./Marlo Runner";
export BUILD_NAME=MarloRunner;
export VERSION=1.0.0;
export PLATFORMS="StandaloneLinux64";
export IS_DEVELOPMENT_BUILD=false;

echo "LICENSE" > /tmp/Unity.ulf;
echo "PROJECT_PATH" > /tmp/project-path;
echo "BUILD_NAME" > /tmp/build-name;
echo "VERSION" > /tmp/version;
echo "PLATFORMS" > /tmp/platforms;
echo "IS_DEVELOPMENT_BUILD" > /tmp/is-development-build;'''
        googleStorageUpload(credentialsId: 'unity-firebuild', bucket: 'gs://${env.TMP_BUCKET}/${env.JOB_NAME}/${env.BUILD_NUMBER}', pattern: '/tmp/Unity.ulf')
        echo 'Initialize complete'
      }
    }

    stage('Build') {
      steps {
        echo 'Build starting .....'
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