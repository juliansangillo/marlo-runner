pipeline {
  agent {
    node {
      label 'jenkins-agent'
    }

  }
  options {
    skipDefaultCheckout(true)
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
export IS_DEVELOPMENT_BUILD=false;

export PLATFORMS="StandaloneLinux64";

echo "Parse platforms to determine build nodes";'''
        echo 'Initialize complete'
      }
    }

    stage('Build') {
      steps {
        echo 'Build starting .....'
        sh '''echo "PROJECT_PATH=$PROJECT_PATH";
ls -l "$PROJECT_PATH";'''
        echo 'Build complete'
      }
    }

  }
  environment {
    SUPPORTED_PLATFORMS_CSV = 'supported-platforms.csv'
    SUPPORTED_PLATFORMS_GS_PATH = 'gs://unity-firebuild-config/$SUPPORTED_PLATFORMS_CSV'
    BUILD_GS_PATH = 'gs://unity-firebuild-artifacts/$JOB_NAME/$BUILD_NUMBER'
  }
}
