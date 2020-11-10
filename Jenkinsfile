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
        sh '''echo "foo, I am initialized!";
echo "$(hostname)";
ls -l;'''
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
    PROJECT_PATH = './Marlo Runner'
  }
}