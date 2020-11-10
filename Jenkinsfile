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

LICENSE > /tmp/Unity.ulf;
PROJECT_PATH > /tmp/project-path;
BUILD_NAME > /tmp/build-name;
VERSION > /tmp/version;
PLATFORMS > /tmp/platforms;
IS_DEVELOPMENT_BUILD > /tmp/is-development-build;'''
        googleStorageUpload(credentialsId: 'unity-firebuild', bucket: 'unity-firebuild-tmp', pattern: '/tmp/Unity.ulf')
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
  }
  options {
    skipDefaultCheckout(true)
  }
}