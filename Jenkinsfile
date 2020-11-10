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
        googleStorageUpload(credentialsId: 'unity-firebuild', bucket: '${TMP_GS_PATH}/Unity.ulf', pattern: '/tmp/Unity.ulf')
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
    SUPPORTED_PLATFORMS_CSV = 'supported-platforms.csv'
    SUPPORTED_PLATFORMS_GS_PATH = 'gs://unity-firebuild-config/$SUPPORTED_PLATFORMS_CSV'
    BUILD_GS_PATH = 'gs://unity-firebuild-artifacts/$JOB_NAME/$BUILD_NUMBER'
    TMP_GS_PATH = 'gs://unity-firebuild-tmp/$JOB_NAME/$BUILD_NUMBER'
  }
  options {
    skipDefaultCheckout(true)
  }
}