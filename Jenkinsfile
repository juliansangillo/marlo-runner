pipeline {
  agent {
    node {
      label 'jenkins-agent'
    }

  }
  stages {
    stage('Initialize') {
      steps {
        sh '''echo "foo, I am initialized ...";
echo "$(hostname)";'''
      }
    }

  }
}
