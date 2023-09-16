import React from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';
import HomepageFeatures from '@site/src/components/HomepageFeatures';

import styles from './index.module.css';
import DeveloperCommunitySection from '../components/DeveloperCommunitySection';
import TechStackSection from '../components/TechStackSection';
import SponorSection from '../components/SponorsAndOrganization';
import FeatureSection from '../components/FeatureSection';

const Icon = require('../../static/img/container.svg').default;
function HomepageHeader() {
  const { siteConfig } = useDocusaurusContext();
  return (
    <header className={clsx('hero hero--primary', styles.heroBanner)}>
      <div className="container">
        <h1 className="hero__title">{siteConfig.title}</h1>
        <p className="hero__subtitle">{siteConfig.tagline}</p>
        <div className={styles.buttons}>
          <Link
            className="button button--secondary button--lg"
            to="/docs/intro"
            style={{
              borderRadius: '25px',
              borderColor: '#426fcc',
              backgroundColor: '#426fcc',
              color: '#fff',
            }}
          >
            Get Started
          </Link>
        </div>
      </div>
    </header>
  );
}

export default function Home(): JSX.Element {
  const { siteConfig } = useDocusaurusContext();
  return (
    <Layout
      title="Home"
      description="A modern logistics management system for the CJ Logistics"
    >
      <HomepageHeader />
      <main>
        <HomepageFeatures />
        <section
          className="page__section"
          style={{ backgroundColor: '#1b1b1d' }}
        >
          <div className="container">
            <div className="row" style={{ gap: '10px', marginTop: '20px' }}>
              <div className="col col--5 col--offset-1">
                <div className="col-demo">
                  <div className="card-demo">
                    <div
                      className="card"
                      style={{
                        backgroundColor: '#242526',
                        color: '#fff',
                      }}
                    >
                      <div className="card__header">
                        <h3
                          className="text--center"
                          style={{ whiteSpace: 'pre-line' }}
                        >
                          The Logistics Platform
                        </h3>
                      </div>
                      <div className="card__body">
                        <p className="text--center">
                          ProfIO is not just a tracking system, it’s a real-time
                          solution that integrates with Google Maps to provide
                          accurate and up-to-the-minute tracking of vehicles
                          including motorbikes, vans, and trucks
                        </p>
                      </div>
                      <div className="card__footer">
                        <Link
                          className={clsx(
                            'button button--primary button--block',
                          )}
                          href="https://github.com/HutechCJ/ProfioApp"
                        >
                          Visit The Project
                        </Link>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div className="col col--5">
                <div className="col-demo">
                  <div className="card-demo">
                    <div
                      className="card"
                      style={{
                        backgroundColor: '#242526',
                        color: '#fff',
                      }}
                    >
                      <div className="card__header">
                        <h3
                          className="text--center"
                          style={{ whiteSpace: 'pre-line' }}
                        >
                          The Logistics Company
                        </h3>
                      </div>
                      <div className="card__body">
                        <p className="text--center">
                          CJ Logistics is a global logistics company that
                          provides comprehensive and extensive logistics
                          services worldwide. CJ Logistics is a global logistics
                          company that provides comprehensive and extensive
                          logistics services worldwide.
                        </p>
                      </div>
                      <div className="card__footer">
                        <Link
                          className={clsx(
                            'button button--primary button--block',
                          )}
                          href="mailto:info@cjvietnam.vn"
                        >
                          Contact The Company
                        </Link>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </section>
        <FeatureSection
          title="Knowledge Base"
          btnLink="https://www.cjlogistics.com/en/network/en-vn"
          btnText="Read More"
          image={Icon}
          direction="right"
        >
          <p>
            We are a global logistics company that provides comprehensive and
            extensive logistics services worldwide.
          </p>
          <p>
            At CJ Logistics, we believe that our people are our greatest asset
            and that our people are the reason for our success.
          </p>
        </FeatureSection>
        <SponorSection />
        <TechStackSection />
        <DeveloperCommunitySection />
      </main>
    </Layout>
  );
}
